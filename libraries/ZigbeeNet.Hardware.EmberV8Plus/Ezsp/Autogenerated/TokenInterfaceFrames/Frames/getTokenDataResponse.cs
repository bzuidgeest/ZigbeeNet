using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterfaceFrames.Structure;

/// <summary>
/// Gets the token data for a single token with provided key
/// Frame value: 0x0102
/// </summary>
public class getTokenDataResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// Token Data
    /// </summary>
    public sl_zigbee_token_data_t tokenData { get; set; }

}
