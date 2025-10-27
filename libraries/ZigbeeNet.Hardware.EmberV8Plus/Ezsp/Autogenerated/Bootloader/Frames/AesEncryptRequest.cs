using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// Perform AES encryption on plaintext using key.
/// Frame value: 0x0094
/// </summary>
public class AesEncryptRequest : EzspFrameRequest
{
    /// <summary>
    /// 16 bytes of plaintext.
    /// </summary>
    public uint8_t[16] plaintext { get; set; }

    /// <summary>
    /// The 16-byte encryption key to use.
    /// </summary>
    public uint8_t[16] key { get; set; }

}
