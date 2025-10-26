using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Structure;

/// <summary>
/// A callback to the GP endpoint to indicate the result of the GPDF transmission.
/// Frame value: 0x00C7
/// </summary>
public class dGpSentHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The handle of the GPDF.
    /// </summary>
    public byte gpepHandle { get; set; }

}
