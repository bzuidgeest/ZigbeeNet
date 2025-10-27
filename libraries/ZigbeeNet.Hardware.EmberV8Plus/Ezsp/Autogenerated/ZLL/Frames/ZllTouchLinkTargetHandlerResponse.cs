using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// This call is fired when the device is a target of a touch link.
/// Frame value: 0x00BB
/// </summary>
public class ZllTouchLinkTargetHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// Information about the network.
    /// </summary>
    public sl_zigbee_zll_network_t networkInfo { get; set; }

}
