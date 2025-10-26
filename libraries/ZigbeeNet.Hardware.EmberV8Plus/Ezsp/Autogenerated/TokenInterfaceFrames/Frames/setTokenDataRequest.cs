using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterfaceFrames.Command;

/// <summary>
/// Sets the token data for a single token with provided key
/// Frame value: 0x0103
/// </summary>
public class setTokenData : EzspFrameRequest
{
    /// <summary>
    /// Key of the token in the token table for which data is to be set.
    /// </summary>
    public uint token { get; set; }

    /// <summary>
    /// Index in case of the indexed token.
    /// </summary>
    public uint index { get; set; }

    /// <summary>
    /// Token Data
    /// </summary>
    public sl_zigbee_token_data_t tokenData { get; set; }

}
