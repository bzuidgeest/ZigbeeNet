using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Sets a manufacturing token in the Customer Information Block (CIB) area of the NCP if that token currently unset (fully erased). Cannot be used with SL_ZIGBEE_EZSP_STACK_CAL_DATA, SL_ZIGBEE_EZSP_STACK_CAL_FILTER, SL_ZIGBEE_EZSP_MFG_ASH_CONFIG, or SL_ZIGBEE_EZSP_MFG_CBKE_DATA token.
/// Frame value: 0x000C
/// </summary>
public class SetMfgTokenResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
