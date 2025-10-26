using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BootloaderFrames.Structure;

/// <summary>
/// Perform AES encryption on plaintext using key.
/// Frame value: 0x0094
/// </summary>
public class aesEncryptResponse : EzspFrameResponse
{
    /// <summary>
    /// 16 bytes of ciphertext.
    /// </summary>
    public uint8_t[16] ciphertext { get; set; }

}
