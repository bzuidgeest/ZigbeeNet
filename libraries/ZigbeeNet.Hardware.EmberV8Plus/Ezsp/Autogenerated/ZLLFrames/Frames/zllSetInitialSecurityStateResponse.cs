using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Structure;

/// <summary>
/// This call will cause the device to setup the security information used in its network. It must be called prior to forming, starting, or joining a network.
/// Frame value: 0x00B3
/// </summary>
public class zllSetInitialSecurityStateResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
