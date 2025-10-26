using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Command;

/// <summary>
/// Return number of active entries in sink table.
/// Frame value: 0x0118
/// </summary>
public class gpSinkTableGetNumberOfActiveEntries : EzspFrameRequest
{
}
