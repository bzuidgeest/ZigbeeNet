using Microsoft.Extensions.Logging;
using SiliconLabs.ASH;
using System;

namespace SiliconLabs.ASH.Common
{
    /// <summary>
    /// Common interface for ASH state machines (v2 and v3)
    /// </summary>
    public interface IAshStateMachine
    {
        /// <summary>
        /// Protocol version
        /// </summary>
        AshVersion Version { get; }
        
        /// <summary>
        /// Current state
        /// </summary>
        AshState State { get; }
        
        /// <summary>
        /// Data received event (application payload)
        /// </summary>
        event Action<byte[]>? OnDataReceived;
        
        /// <summary>
        /// State changed event
        /// </summary>
        event Action<AshState>? OnStateChanged;
        
        /// <summary>
        /// Error occurred event
        /// </summary>
        event Action<string>? OnError;
        
        /// <summary>
        /// Frame to send event
        /// </summary>
        event Action<IAshFrame>? OnFrameToSend;
        
        /// <summary>
        /// Initialize connection
        /// </summary>
        void Connect();
        
        /// <summary>
        /// Disconnect
        /// </summary>
        void Disconnect();
        
        /// <summary>
        /// Send application data
        /// </summary>
        bool SendData(byte[] data);
        
        /// <summary>
        /// Send empty acknowledgement
        /// </summary>
        void SendEmptyAck();
        
        /// <summary>
        /// Process received frame
        /// </summary>
        void ProcessReceivedFrame(IAshFrame frame);
        
        /// <summary>
        /// Process retransmission timers
        /// </summary>
        void ProcessTimers();
        
        /// <summary>
        /// Check if there are pending frames
        /// </summary>
        bool HasPendingFrames { get; }
    }
}
