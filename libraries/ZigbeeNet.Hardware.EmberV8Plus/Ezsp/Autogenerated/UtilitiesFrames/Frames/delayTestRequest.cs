using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Used to test that UART flow control is working correctly.
/// Frame value: 0x009D
/// </summary>
public class delayTest : EzspFrameRequest
{
    /// <summary>
    /// Data will not be read from the host for this many milliseconds.
    /// </summary>
    public ushort delay { get; set; }

}
