using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Allows the HOST to know whether the NCP is running the XNCP library. If so, the response contains also the manufacturer ID and the version number of the XNCP application that is running on the NCP.
/// Frame value: 0x0013
/// </summary>
public class getXncpInfo : EzspFrameRequest
{
}
