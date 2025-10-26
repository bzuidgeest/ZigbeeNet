using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Command;

/// <summary>
/// Retrieve information about the current and alternate network key, excluding their contents.
/// Frame value: 0x0116
/// </summary>
public class secManGetNetworkKeyInfo : EzspFrameRequest
{
}
