using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MfglibFrames.Structure;

/// <summary>
/// Stops transmitting a random stream of characters started by mfglibInternalStartStream().
/// Frame value: 0x0088
/// </summary>
public class mfglibInternalStopStreamResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
