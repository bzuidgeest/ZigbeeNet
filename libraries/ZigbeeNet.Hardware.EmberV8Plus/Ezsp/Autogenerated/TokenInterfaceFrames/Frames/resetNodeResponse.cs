using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterfaceFrames.Structure;

/// <summary>
/// Reset the node by calling halReboot.
/// Frame value: 0x0104
/// </summary>
public class resetNodeResponse : EzspFrameResponse
{
}
