using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Sets a token (8 bytes of non-volatile storage) in the Simulated EEPROM of the NCP.
/// Frame value: 0x0009
/// </summary>
public class SetTokenRequest : EzspFrameRequest
{
    /// <summary>
    /// Which token to set
    /// </summary>
    public byte tokenId { get; set; }

    /// <summary>
    /// The data to write to the token.
    /// </summary>
    public uint8_t[8] tokenData { get; set; }

