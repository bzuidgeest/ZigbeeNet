using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Retrieve metadata about an APS link key.  Does not retrieve contents.
/// Frame value: 0x010C
/// </summary>
public class SecManGetApsKeyInfoRequest : EzspFrameRequest
{
    /// <summary>
    /// Context used to input information about key.
    /// </summary>
    public sl_zigbee_sec_man_context_t context { get; set; }

}
