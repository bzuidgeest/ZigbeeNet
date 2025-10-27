using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Retrieves a manufacturing token from the Flash Information Area of the NCP (except for SL_ZIGBEE_EZSP_STACK_CAL_DATA which is managed by the stack).
/// Frame value: 0x000B
/// </summary>
public class GetMfgTokenRequest : EzspFrameRequest
{
    /// <summary>
    /// Which manufacturing token to read.
    /// </summary>
    public sl_zigbee_ezsp_mfg_token_id_t tokenId { get; set; }

}
