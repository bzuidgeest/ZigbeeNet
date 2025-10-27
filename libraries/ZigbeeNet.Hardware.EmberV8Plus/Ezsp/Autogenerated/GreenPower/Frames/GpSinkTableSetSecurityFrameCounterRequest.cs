using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Sets security framecounter in the sink table
/// Frame value: 0x00F5
/// </summary>
public class GpSinkTableSetSecurityFrameCounterRequest : EzspFrameRequest
{
    /// <summary>
    /// Index to the Sink table
    /// </summary>
    public byte index { get; set; }

    /// <summary>
    /// Security Frame Counter
    /// </summary>
    public uint sfc { get; set; }

}
