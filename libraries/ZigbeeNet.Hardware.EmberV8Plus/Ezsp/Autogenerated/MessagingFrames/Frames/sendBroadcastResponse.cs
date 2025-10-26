using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Structure;

/// <summary>
/// Sends a broadcast message as per the ZigBee specification.
/// Frame value: 0x0036
/// </summary>
public class sendBroadcastResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The APS sequence number that will be used when this message is transmitted.
    /// </summary>
    public byte apsSequence { get; set; }

}
