using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Retrieves a token (8 bytes of non-volatile storage) from the Simulated EEPROM of the NCP.
/// Frame value: 0x000A
/// </summary>
public class getTokenResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The contents of the token.
    /// </summary>
    public uint8_t[8] tokenData { get; set; }

}
