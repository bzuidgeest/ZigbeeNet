using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterfaceFrames.Structure;

/// <summary>
/// Gets the token information for a single token at provided index
/// Frame value: 0x0101
/// </summary>
public class getTokenInfoResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// Token information.
    /// </summary>
    public sl_zigbee_token_info_t tokenInfo { get; set; }

}
