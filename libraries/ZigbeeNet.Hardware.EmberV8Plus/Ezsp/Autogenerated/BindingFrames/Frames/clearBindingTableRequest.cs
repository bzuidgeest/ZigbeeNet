using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BindingFrames.Command;

/// <summary>
/// Deletes all binding table entries.
/// Frame value: 0x002A
/// </summary>
public class clearBindingTable : EzspFrameRequest
{
}
