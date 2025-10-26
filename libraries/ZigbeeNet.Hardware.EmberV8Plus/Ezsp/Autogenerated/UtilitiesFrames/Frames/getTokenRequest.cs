using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Retrieves a token (8 bytes of non-volatile storage) from the Simulated EEPROM of the NCP.
/// Frame value: 0x000A
/// </summary>
public class getToken : EzspFrameRequest
{
    /// <summary>
    /// Which token to read
    /// </summary>
    public byte tokenId { get; set; }

}
