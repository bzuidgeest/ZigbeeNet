using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Mfglib.Frames;

/// <summary>
/// Starts transmitting an unmodulated tone on the currently set channel and power level. Upon successful return, the tone will be transmitting. To stop transmitting tone, application must call mfglibInternalStopTone(), allowing it the flexibility to determine its own criteria for tone duration (time, event, etc.)
/// Frame value: 0x0085
/// </summary>
public class MfglibInternalStartToneResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
