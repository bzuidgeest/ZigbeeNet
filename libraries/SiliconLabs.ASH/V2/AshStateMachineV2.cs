using Microsoft.Extensions.Logging;
using SiliconLabs.ASH;
using SiliconLabs.ASH.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SiliconLabs.ASH.V2
{
    /// <summary>
    /// ASH v2 state machine implementation (UG101)
    /// Handles DATA frames, nRdy flow control, and ERROR frames
    /// </summary>
    public class AshStateMachineV2 : IAshStateMachine
    {
        private AshState _state = AshState.Disconnected;
        private readonly object _stateLock = new object();
        private readonly ILogger<AshStateMachineV2> _logger;
        
        // Sequence numbers (1-7 per spec)
        private byte _txFrmNum = 1;  // Our outgoing frame number
        private byte _rxAckNum = 1;  // Expected ackNum from NCP (next frame we expect)
        
        // Pending frames (up to WindowSize per spec)
        private readonly Queue<PendingFrame> _pendingFrames = new Queue<PendingFrame>();
        private Stopwatch _retransmitTimer = new Stopwatch();
        
        public AshVersion Version => AshVersion.V2;
        
        public event Action<byte[]>? OnDataReceived;
        public event Action<AshState>? OnStateChanged;
        public event Action<string>? OnError;
        public event Action<IAshFrame>? OnFrameToSend;
        
        private class PendingFrame
        {
            public AshFrameV2 Frame { get; set; } = null!;
            public int RetryCount { get; set; }
        }
        
        public AshStateMachineV2(ILogger<AshStateMachineV2> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public AshState State
        {
            get
            {
                lock (_stateLock)
                {
                    return _state;
                }
            }
            private set
            {
                lock (_stateLock)
                {
                    if (_state != value)
                    {
                        _logger.LogInformation("State changing from {OldState} to {NewState}", _state, value);
                        _state = value;
                        OnStateChanged?.Invoke(_state);
                    }
                }
            }
        }
        
        public bool HasPendingFrames
        {
            get
            {
                lock (_stateLock)
                {
                    return _pendingFrames.Count > 0;
                }
            }
        }
        
        public void Connect()
        {
            lock (_stateLock)
            {
                if (_state != AshState.Disconnected && _state != AshState.Failed)
                {
                    _logger.LogWarning("Already connected or connecting");
                    OnError?.Invoke("Already connected or connecting");
                    return;
                }
                
                _logger.LogInformation("Initiating connection - sending RST frame");
                
                // Reset state
                _txFrmNum = 1;
                _rxAckNum = 1;
                _pendingFrames.Clear();
                
                State = AshState.Connecting;
                
                // Send RST frame
                var rstFrame = AshFrameV2.CreateResetFrame();
                OnFrameToSend?.Invoke(rstFrame);
            }
        }
        
        public void Disconnect()
        {
            lock (_stateLock)
            {
                _logger.LogInformation("Disconnecting");
                State = AshState.Disconnected;
                _txFrmNum = 1;
                _rxAckNum = 1;
                _pendingFrames.Clear();
                _retransmitTimer.Stop();
            }
        }
        
        public bool SendData(byte[] data)
        {
            lock (_stateLock)
            {
                if (_state != AshState.Connected)
                {
                    _logger.LogWarning("Cannot send data: Not connected");
                    OnError?.Invoke("Not connected");
                    return false;
                }
                
                if (_pendingFrames.Count >= AshConstantsV2.WindowSize)
                {
                    _logger.LogWarning("Send window full ({WindowSize} frames pending)", AshConstantsV2.WindowSize);
                    OnError?.Invoke("Send window full (" + AshConstantsV2.WindowSize + " frames pending)");
                    return false;
                }
                
                if (data.Length > AshConstantsV2.MaxDataLength)
                {
                    _logger.LogError("Data length {Length} exceeds maximum {MaxLength}", data.Length, AshConstantsV2.MaxDataLength);
                    OnError?.Invoke("Data too large. Max: " + AshConstantsV2.MaxDataLength + " bytes");
                    return false;
                }
                
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("Queuing DATA frame with {ByteCount} bytes [frmNum={FrmNum}, ackNum={AckNum}]", 
                        data.Length, _txFrmNum, _rxAckNum);
                }
                
                // Create DATA frame
                var dataFrame = AshFrameV2.CreateDataFrame(_txFrmNum, _rxAckNum, data);
                
                // Store for retransmission
                _pendingFrames.Enqueue(new PendingFrame { Frame = dataFrame, RetryCount = 0 });
                
                // Start/restart timer
                _retransmitTimer.Restart();
                
                // Send frame
                OnFrameToSend?.Invoke(dataFrame);
                
                // Increment frmNum for next frame
                _txFrmNum = IncrementCounter(_txFrmNum);
                
                return true;
            }
        }
        
        public void SendEmptyAck()
        {
            lock (_stateLock)
            {
                if (_state != AshState.Connected)
                    return;
                
                if (_logger.IsEnabled(LogLevel.Trace))
                {
                    _logger.LogTrace("Sending empty ACK [ackNum={AckNum}]", _rxAckNum);
                }
                
                var ackFrame = AshFrameV2.CreateAckFrame(_rxAckNum);
                OnFrameToSend?.Invoke(ackFrame);
            }
        }
        
        public void ProcessReceivedFrame(IAshFrame frame)
        {
            lock (_stateLock)
            {
                var frameV2 = frame as AshFrameV2;
                if (frameV2 == null)
                {
                    _logger.LogWarning("Received non-v2 frame");
                    return;
                }
                
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("Processing {FrameType} [frmNum={FrmNum}, ackNum={AckNum}, Payload={PayloadSize}]", 
                        frameV2.FrameType, frameV2.OutgoingFrameCounter, frameV2.AckNackFrameCounter, frameV2.Data.Length);
                }
                
                switch (_state)
                {
                    case AshState.Connecting:
                        HandleConnectingState(frameV2);
                        break;
                        
                    case AshState.Connected:
                        HandleConnectedState(frameV2);
                        break;
                        
                    default:
                        _logger.LogWarning("Unexpected frame {FrameType} in state {State}", frameV2.FrameType, _state);
                        OnError?.Invoke("Unexpected frame in state " + _state);
                        break;
                }
            }
        }
        
        public void ProcessTimers()
        {
            lock (_stateLock)
            {
                if (_state != AshState.Connected || _pendingFrames.Count == 0)
                    return;
                
                if (_retransmitTimer.ElapsedMilliseconds >= AshConstantsV2.RetransmissionTimeoutMs)
                {
                    _logger.LogWarning("Retransmission timeout ({TimeoutMs}ms) - {PendingCount} frame(s) pending", 
                        AshConstantsV2.RetransmissionTimeoutMs, _pendingFrames.Count);
                    RetransmitPendingFrames();
                }
            }
        }
        
        private void HandleConnectingState(AshFrameV2 frame)
        {
            if (frame.FrameType == AshFrameType.ResetAck)
            {
                _logger.LogInformation("Received RSTACK - connection established (reset code: {ResetCode})", frame.ResetCode);
                
                State = AshState.Connected;
                _txFrmNum = 1;
                _rxAckNum = 1;
                
                // v2: RSTACK doesn't carry application data
            }
        }
        
        private void HandleConnectedState(AshFrameV2 frame)
        {
            // Process acknowledgements (ackNum field acknowledges our frames)
            if (frame.FrameType == AshFrameType.Data || 
                frame.FrameType == AshFrameType.Ack || 
                frame.FrameType == AshFrameType.Nak)
            {
                ProcessAcknowledgement(frame.AckNackFrameCounter);
            }
            
            switch (frame.FrameType)
            {
                case AshFrameType.Data:
                    HandleDataFrame(frame);
                    break;
                    
                case AshFrameType.Ack:
                    // ACK received - acknowledgements already processed above
                    break;
                    
                case AshFrameType.Nak:
                    HandleNakFrame(frame);
                    break;
                    
                case AshFrameType.ResetAck:
                    _logger.LogWarning("Unexpected RSTACK - connection reset by NCP");
                    OnError?.Invoke("Unexpected RSTACK - connection reset by NCP");
                    State = AshState.Disconnected;
                    break;
                    
                case AshFrameType.Reset:
                    _logger.LogWarning("Received RST from NCP - reinitializing");
                    OnError?.Invoke("Received RST from NCP");
                    _txFrmNum = 1;
                    _rxAckNum = 1;
                    _pendingFrames.Clear();
                    var rstackFrame = AshFrameV2.CreateResetAckFrame(AshConstantsV2.ResetCode_Software);
                    OnFrameToSend?.Invoke(rstackFrame);
                    break;
                    
                case AshFrameType.Error:
                    _logger.LogError("ERROR frame received - NCP in FAILED state. Code: {Code}", frame.ResetCode);
                    OnError?.Invoke("NCP FAILED: error code " + frame.ResetCode);
                    State = AshState.Failed;
                    break;
            }
        }
        
        private void HandleDataFrame(AshFrameV2 frame)
        {
            // Check if this is the expected frame
            if (frame.OutgoingFrameCounter == _rxAckNum)
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug("DATA frame: sequence correct (frmNum={FrmNum}), delivering {ByteCount} bytes", 
                        frame.OutgoingFrameCounter, frame.Data.Length);
                }
                
                OnDataReceived?.Invoke(frame.Data);
                _rxAckNum = IncrementCounter(_rxAckNum);
                SendEmptyAck();
            }
            else
            {
                _logger.LogWarning("DATA frame out of sequence: expected frmNum={Expected}, got {Actual} - sending NAK", 
                    _rxAckNum, frame.OutgoingFrameCounter);
                
                var nakFrame = AshFrameV2.CreateNakFrame(_rxAckNum);
                OnFrameToSend?.Invoke(nakFrame);
            }
        }
        
        private void HandleNakFrame(AshFrameV2 frame)
        {
            _logger.LogWarning("NAK received - retransmitting pending frames");
            RetransmitPendingFrames();
        }
        
        private void ProcessAcknowledgement(byte ackNum)
        {
            // Remove acknowledged frames from pending queue
            while (_pendingFrames.Count > 0)
            {
                var pending = _pendingFrames.Peek();
                
                // Check if this frame has been acknowledged
                // ackNum acknowledges frames up to, but not including, ackNum
                byte lastAcked = ackNum == 1 ? (byte)7 : (byte)(ackNum - 1);
                
                if (IsCounterInRange(pending.Frame.OutgoingFrameCounter, lastAcked))
                {
                    if (_logger.IsEnabled(LogLevel.Trace))
                    {
                        _logger.LogTrace("Frame {FrmNum} acknowledged by ackNum={AckNum}", 
                            pending.Frame.OutgoingFrameCounter, ackNum);
                    }
                    
                    _pendingFrames.Dequeue();
                }
                else
                {
                    break;
                }
            }
            
            // Restart timer if frames still pending
            if (_pendingFrames.Count > 0)
            {
                _retransmitTimer.Restart();
            }
            else
            {
                _retransmitTimer.Stop();
            }
        }
        
        private void RetransmitPendingFrames()
        {
            if (_pendingFrames.Count == 0)
                return;
            
            List<PendingFrame> toRetransmit = new List<PendingFrame>(_pendingFrames);
            
            foreach (var pending in toRetransmit)
            {
                pending.RetryCount++;
                
                if (pending.RetryCount > AshConstantsV2.MaxRetries)
                {
                    _logger.LogError("Max retries ({MaxRetries}) exceeded", AshConstantsV2.MaxRetries);
                    OnError?.Invoke("Max retries (" + AshConstantsV2.MaxRetries + ") exceeded");
                    State = AshState.Failed;
                    _pendingFrames.Clear();
                    _retransmitTimer.Stop();
                    return;
                }
                
                _logger.LogInformation("Retransmitting frame {FrmNum} (attempt {Attempt}/{MaxRetries})", 
                    pending.Frame.OutgoingFrameCounter, pending.RetryCount, AshConstantsV2.MaxRetries);
                
                // Update ackNum in frame before retransmitting
                pending.Frame.AckNackFrameCounter = _rxAckNum;
                pending.Frame.IsRetransmit = true;
                OnFrameToSend?.Invoke(pending.Frame);
            }
            
            _retransmitTimer.Restart();
        }
        
        private byte IncrementCounter(byte counter)
        {
            counter++;
            if (counter > AshConstantsV2.MaxFrameCounter)
                counter = AshConstantsV2.MinFrameCounter;
            return counter;
        }
        
        private bool IsCounterInRange(byte counter, byte target)
        {
            // Simple equality check for acknowledgement
            return counter == target;
        }
    }
}
