using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SiliconLabs.ASH;
using SiliconLabs.ASH.Common;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Types;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

public partial class EmberNcp
{
	//public Version CurrentNCPVersion { get; set; }

	private readonly ConcurrentDictionary<int, PendingEmberRequest> _pending = new();
	private readonly AshHost _ashHost;
	private readonly CancellationTokenSource _shutDownCancellationToken = new CancellationTokenSource();
	private Task _incomingFrameTask;
	//private IAsyncEnumerable<IAshFrame> _incomingFrames;

	public event Action<int, byte[]>? UnsolicitedMessage;

	public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(1);

	public EmberNcp(AshHost ashHost)
	{
		_ashHost = ashHost;

		_incomingFrameTask = Task.Run(() => HandleIncomingFrame(_shutDownCancellationToken.Token));
	}

	/*public async Task<Version> Version2(byte desiredProtocolVersion, CancellationToken cancellationToken = default)
	{
		VersionRequest request = new VersionRequest
		{
			DesiredProtocolVersion = desiredProtocolVersion
		};

		VersionResponse? response = await SendFrameAsync(request.SequenceNumber, request.GetFrameBytes(), false, cancellationToken) as VersionResponse;

		//VersionResponse? response = VersionResponse.Parse(result[0]) as VersionResponse;
		_logger.LogDebug(response?.ToString());
		return new Version(response.ProtocolVersion, response.StackType, response.StackVersion);
	}*/

	private async Task<EzspFrameResponseV8Plus> SendFrameAsync(
		int sequence,
		byte[] packet,
		bool expectMultiple,
		CancellationToken cancellationToken = default)
	{
		PendingEmberRequest request = new PendingEmberRequest { };

		// Use TryAdd to detect collisions
		if (!_pending.TryAdd(sequence, request))
		{
			throw new InvalidOperationException($"Duplicate sequence number {sequence} - previous request still pending");
		}

		try
		{
			// Send the packet
			bool sendSuccess = _ashHost.SendData(packet);
			if (!sendSuccess)
			{
				throw new IOException("Failed to send data to NCP");
			}

			// Timeout handling with proper disposal
			using CancellationTokenSource timeoutCts = new CancellationTokenSource(Timeout);
			using CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
				timeoutCts.Token,
				cancellationToken,
				_shutDownCancellationToken.Token);

			using var registration = linkedCts.Token.Register(() =>
			{
				if (_pending.TryRemove(sequence, out PendingEmberRequest? req))
				{
					if (timeoutCts.Token.IsCancellationRequested)
						req.TaskCompletionSource.TrySetException(new TimeoutException($"Request {sequence} timed out after {Timeout}"));
					else if (_shutDownCancellationToken.Token.IsCancellationRequested)
						req.TaskCompletionSource.TrySetCanceled();
					else
						req.TaskCompletionSource.TrySetCanceled(cancellationToken);
				}
			});

			return await request.TaskCompletionSource.Task;
		}
		catch
		{
			// Clean up on any exception
			_pending.TryRemove(sequence, out _);
			throw;
		}
	}

	public void Dispose()
	{
		_shutDownCancellationToken.Cancel();
		_incomingFrameTask?.Wait(TimeSpan.FromSeconds(2));
		_shutDownCancellationToken.Dispose();
	}

	/// <summary>
	/// Continuously processes incoming frames from the ASH host
	/// </summary>
	private async Task HandleIncomingFrame(CancellationToken cancellationToken)
	{
		_logger.LogDebug("Incoming frame handler started");
		try
		{
			await foreach (IAshFrame ashFrame in _ashHost.GetIncomingFrameStream(cancellationToken))
			{
				EmberResponseHeader emberResponseHeader = EzspFrameResponseV8Plus.ParseHeader(ashFrame.Data);
				try
				{
					// Parse the EZSP frame from ASH payload
					EzspFrameResponseV8Plus? ezspResponse = EzspFrameV8Plus.CreateHandler(ashFrame.Data);

					if (ezspResponse == null)
					{
						_logger.LogWarning("Failed to parse EZSP frame from ASH data");
						continue;
					}

					// Use EZSP sequence number (not ASH frame counter!)
					int sequence = ezspResponse.SequenceNumber;

					// Try to match with pending request
					if (_pending.TryRemove(sequence, out PendingEmberRequest? request))
					{
						// This is a response to a pending request
						request.Replies.Add(ashFrame.Data);

						// Check if we expect multiple responses
						// For now, complete on first response
						// TODO: Implement proper multi-response handling based on frame type
						request.TaskCompletionSource.TrySetResult(request.Replies);

						_logger.LogDebug("Matched response for sequence {Sequence}", sequence);
					}
					else
					{
						// Unsolicited message (callback, event, etc.)
						_logger.LogDebug("Unsolicited message received: sequence {Sequence}, type {Type}",
							sequence, ezspResponse.GetType().Name);

						UnsolicitedMessage?.Invoke(sequence, ashFrame.Data);
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error processing incoming frame");
				}
			}
		}
		catch (OperationCanceledException)
		{
			_logger.LogInformation("Incoming frame handler cancelled");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Fatal error in incoming frame handler");
		}
		finally
		{
			// Cancel all pending requests on shutdown
			foreach (var kvp in _pending)
			{
				if (_pending.TryRemove(kvp.Key, out var request))
				{
					request.TaskCompletionSource.TrySetCanceled();
				}
			}
		}
	}
}
