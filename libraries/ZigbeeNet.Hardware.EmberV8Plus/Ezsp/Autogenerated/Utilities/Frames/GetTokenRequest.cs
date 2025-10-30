using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Retrieves a token (8 bytes of non-volatile storage) from the Simulated EEPROM of the NCP.
/// Frame value: 0x000A
/// </summary>
public class GetTokenRequest : EzspFrameRequest
{
    /// <summary>
    /// Which token to read
    /// </summary>
    public byte tokenId { get; set; }

