using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// This function sends an APS TransportKey command containing the current trust center link key. The node to which the command is sent is specified via the short and long address arguments.
/// Frame value: 0x0067
/// </summary>
public class sendTrustCenterLinkKeyResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success of failure of the operation
    /// </summary>
    public sl_status_t status { get; set; }

}
