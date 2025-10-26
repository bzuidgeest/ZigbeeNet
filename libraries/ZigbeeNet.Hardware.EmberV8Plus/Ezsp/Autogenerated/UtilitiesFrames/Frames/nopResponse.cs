using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// A command which does nothing. The Host can use this to set the sleep mode or to check the status of the NCP.
/// Frame value: 0x0005
/// </summary>
public class nopResponse : EzspFrameResponse
{
}
