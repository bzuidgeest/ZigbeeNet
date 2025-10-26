using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterfaceFrames.Structure;

/// <summary>
/// Run GP security test vectors.
/// Frame value: 0x0117
/// </summary>
public class gpSecurityTestVectorsResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
