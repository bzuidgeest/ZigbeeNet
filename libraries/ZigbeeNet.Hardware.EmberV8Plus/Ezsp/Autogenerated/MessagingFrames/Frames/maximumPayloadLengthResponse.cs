using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Returns the maximum size of the payload. The size depends on the security level in use.
/// Frame value: 0x0033
/// </summary>
public class maximumPayloadLengthResponse : EzspFrameResponse
{
    /// <summary>
    /// The maximum APS payload length.
    /// </summary>
    public byte apsLength { get; set; }

}
