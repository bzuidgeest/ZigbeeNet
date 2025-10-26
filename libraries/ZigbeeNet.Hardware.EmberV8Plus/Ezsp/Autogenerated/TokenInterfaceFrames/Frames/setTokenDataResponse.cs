using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterfaceFrames.Structure;

/// <summary>
/// Sets the token data for a single token with provided key
/// Frame value: 0x0103
/// </summary>
public class setTokenDataResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
