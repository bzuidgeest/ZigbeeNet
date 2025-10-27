using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Sets a manufacturing token in the Customer Information Block (CIB) area of the NCP if that token currently unset (fully erased). Cannot be used with SL_ZIGBEE_EZSP_STACK_CAL_DATA, SL_ZIGBEE_EZSP_STACK_CAL_FILTER, SL_ZIGBEE_EZSP_MFG_ASH_CONFIG, or SL_ZIGBEE_EZSP_MFG_CBKE_DATA token.
/// Frame value: 0x000C
/// </summary>
public class SetMfgTokenRequest : EzspFrameRequest
{
    /// <summary>
    /// Which manufacturing token to set.
    /// </summary>
    public sl_zigbee_ezsp_mfg_token_id_t tokenId { get; set; }

    /// <summary>
    /// The length of the &lt;i&gt;tokenData&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte tokenDataLength { get; set; }

    /// <summary>
    /// The manufacturing token data.
    /// </summary>
    public uint8_t[tokenDataLength] tokenData { get; set; }

}
