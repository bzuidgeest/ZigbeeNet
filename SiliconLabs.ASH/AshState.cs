namespace SiliconLabs.ASH
{
    /// <summary>
    /// ASH protocol state
    /// </summary>
    public enum AshState
    {
        /// <summary>
        /// Initial state - disconnected
        /// </summary>
        Disconnected,
        
        /// <summary>
        /// Connecting - waiting for RSTACK
        /// </summary>
        Connecting,
        
        /// <summary>
        /// Connected and ready to transfer data
        /// </summary>
        Connected,
        
        /// <summary>
        /// Failed state
        /// </summary>
        Failed
    }
}
