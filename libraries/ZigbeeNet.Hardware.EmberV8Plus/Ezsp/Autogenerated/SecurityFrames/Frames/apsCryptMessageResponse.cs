using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// Encrypt/decrypt a message in-place using APS.
/// Frame value: 0x0129
/// </summary>
public class apsCryptMessageResponse : EzspFrameResponse
{
    /// <summary>
    /// Status of the encryption/decryption call.
    /// </summary>
    public sl_status_t status { get; set; }

}
