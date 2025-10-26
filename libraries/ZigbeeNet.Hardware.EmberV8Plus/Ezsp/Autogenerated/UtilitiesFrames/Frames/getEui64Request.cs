using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Returns the EUI64 ID of the local node.
/// Frame value: 0x0026
/// </summary>
public class getEui64 : EzspFrameRequest
{
}
