using Microsoft.Extensions.Logging;
using SiliconLabs.ASH;
using SiliconLabs.ASH.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SiliconLabs.ASH.V3
{
    /// <summary>
    /// ASH v3 state machine implementation (UG115)
    /// Per spec: Frame counters range from 1-7, max 2 frames in flight
    /// v3: Data carried in ACK/NAK/RESET_ACK frames (no separate DATA frame type)
    /// </summary>
    public class AshStateMachineV3 : IAshStateMachine
    {
        private AshState _state = AshState.Disconnected;
        private readonly object _stateLock = new object();
        private readonly ILogger<AshStateMachineV3> _logger;
        
        // Sequence numbers (1-7 per spec)
        private byte _txOfc = 1;  // Our outgoing frame counter
        private byte _rxAfc = 1;  // Expected AFC from NCP
        
        // Pending frames (up to 2 per spec)
        private readonly Queue<PendingFrame> _pendingFrames = new Queue<PendingFrame>();
        private Stopwatch _retransmitTimer = new Stopwatch();
        
        public AshVersion Version => AshVersion.V3;
        
        public event Action<IAshFrame>? OnDataReceived;
        public event Action<AshState>? OnStateChanged;
        public event Action<string>? OnError;
        public event Action<IAshFrame>? OnFrameToSend;
        
        private class PendingFrame
        {
            public AshFrameV3 Frame { get; set; } = null!;
            public int RetryCount { get; set; }
        }
        
        public AshStateMachineV3(ILogger<AshStateMachineV3> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public AshState State
        {
            get { lock (_stateLock) { return _state; } }
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
            get { lock (_stateLock) { return _pendingFrames.Count > 0; } }
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
                
                _logger.LogInformation("Initiating connection - sending RESET frame");
                
                // Reset state
                _txOfc = 1;
                _rxAfc = 1;
                _pendingFrames.Clear();
                
                State = AshState.Connecting;
                
                // Send RST frame
                var rstFrame = AshFrameV3.CreateResetFrame();
                OnFrameToSend?.Invoke(rstFrame);
            }
        }
        
        public void Disconnect()
        {
            lock (_stateLock)
            {
                _logger.LogInformation("Disconnecting");
                State = AshState.Disconnected;
                _txOfc = 1;
                _rxAfc = 1;
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
                
                if (_pendingFrames.Count >= AshConstantsV3.WindowSize)
                {
                    _logger.LogWarning("Send window full");
                    OnError?.Invoke("Send window full");
                    return false;
                }
                
                if (data.Length > AshConstantsV3.MaxDataLength)
                {
                    _logger.LogError("Data too large. Max: {Max} bytes", AshConstantsV3.MaxDataLength);
                    OnError?.Invoke("Data too large");
                    return false;
                }
                
                // Create ACK frame with data (v3 carries data in ACK frames)
                var ackFrame = AshFrameV3.CreateAckFrame(_txOfc, _rxAfc, data);
                
                // Store for retransmission
                _pendingFrames.Enqueue(new PendingFrame { Frame = ackFrame, RetryCount = 0 });
                
                // Start/restart timer
                _retransmitTimer.Restart();
                
                // Send frame
                OnFrameToSend?.Invoke(ackFrame);
                
                // Increment OFC
                _txOfc = IncrementCounter(_txOfc);
                
                return true;
            }
        }
        
        public void SendEmptyAck()
        {
            lock (_stateLock)
            {
                if (_state != AshState.Connected)
                    return;
                
                byte currentOfc = _txOfc == 1 ? (byte)7 : (byte)(_txOfc - 1);
                var ackFrame = AshFrameV3.CreateAckFrame(currentOfc, _rxAfc);
                OnFrameToSend?.Invoke(ackFrame);
            }
        }
        
        public void ProcessReceivedFrame(IAshFrame frame)
        {
            lock (_stateLock)
            {
                switch (_state)
                {
                    case AshState.Connecting:
                        if (frame.FrameType == AshFrameType.ResetAck)
                        {
                            _logger.LogInformation("Received RESET_ACK - connection established");
                            State = AshState.Connected;
                            _txOfc = 1;
                            _rxAfc = frame.HasPayload ? IncrementCounter(frame.OutgoingFrameCounter) : (byte)1;
                            
                            if (frame.HasPayload)
                            {
                                OnDataReceived?.Invoke(frame);
                                SendEmptyAck();
                            }
                        }
                        break;
                        
                    case AshState.Connected:
                        // Process acknowledgements
                        ProcessAcknowledgement(frame.AckNackFrameCounter);
                        
                        // Handle frame types
                        if (frame.FrameType == AshFrameType.Ack || frame.FrameType == AshFrameType.Nak)
                        {
                            if (frame.HasPayload)
                            {
                                if (frame.OutgoingFrameCounter == _rxAfc)
                                {
                                    OnDataReceived?.Invoke(frame);
                                    _rxAfc = IncrementCounter(_rxAfc);
                                    SendEmptyAck();
                                }
                                else
                                {
                                    _logger.LogWarning("Frame out of sequence");
                                    byte currentOfc = _txOfc == 1 ? (byte)7 : (byte)(_txOfc - 1);
                                    var nakFrame = AshFrameV3.CreateNakFrame(currentOfc, _rxAfc);
                                    OnFrameToSend?.Invoke(nakFrame);
                                }
                            }
                            
                            if (frame.FrameType == AshFrameType.Nak)
                            {
                                _logger.LogWarning("NAK received - retransmitting");
                                RetransmitPendingFrames();
                            }
                        }
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
                
                if (_retransmitTimer.ElapsedMilliseconds >= AshConstantsV3.RetransmissionTimeoutMs)
                {
                    _logger.LogWarning("Retransmission timeout");
                    RetransmitPendingFrames();
                }
            }
        }
        
        private void ProcessAcknowledgement(byte afc)
        {
            while (_pendingFrames.Count > 0)
            {
                var pending = _pendingFrames.Peek();
                byte lastAcked = afc == 1 ? (byte)7 : (byte)(afc - 1);
                
                if (pending.Frame.OutgoingFrameCounter == lastAcked)
                {
                    _pendingFrames.Dequeue();
                }
                else
                {
                    break;
                }
            }
            
            if (_pendingFrames.Count > 0)
                _retransmitTimer.Restart();
            else
                _retransmitTimer.Stop();
        }
        
        private void RetransmitPendingFrames()
        {
            if (_pendingFrames.Count == 0)
                return;
            
            List<PendingFrame> toRetransmit = new List<PendingFrame>(_pendingFrames);
            
            foreach (var pending in toRetransmit)
            {
                pending.RetryCount++;
                
                if (pending.RetryCount > AshConstantsV3.MaxRetries)
                {
                    _logger.LogError("Max retries exceeded");
                    OnError?.Invoke("Max retries exceeded");
                    State = AshState.Failed;
                    _pendingFrames.Clear();
                    _retransmitTimer.Stop();
                    return;
                }
                
                // Update AFC and resend
                pending.Frame.AckNackFrameCounter = _rxAfc;
                OnFrameToSend?.Invoke(pending.Frame);
            }
            
            _retransmitTimer.Restart();
        }
        
        private byte IncrementCounter(byte counter)
        {
            counter++;
            if (counter > AshConstantsV3.MaxFrameCounter)
                counter = AshConstantsV3.MinFrameCounter;
            return counter;
        }
    }
}
