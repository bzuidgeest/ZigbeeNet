using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Returns the maximum size of the payload. The size depends on the security level in use.
/// Frame value: 0x0033
/// </summary>
public class MaximumPayloadLengthResponse : EzspFrameResponse
{
    /// <summary>
    /// The maximum APS payload length.
    /// </summary>
    public byte apsLength { get; set; }

}
