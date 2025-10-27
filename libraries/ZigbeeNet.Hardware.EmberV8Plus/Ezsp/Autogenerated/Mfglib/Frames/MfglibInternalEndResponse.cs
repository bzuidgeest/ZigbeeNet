using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Mfglib.Frames;

/// <summary>
/// Deactivate use of mfglib test routines; restores the hardware to the state it was in prior to mfglibInternalStart() and stops receiving packets started by mfglibInternalStart() at the same time.
/// Frame value: 0x0084
/// </summary>
public class MfglibInternalEndResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
