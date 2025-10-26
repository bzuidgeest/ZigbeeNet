using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

public class getParentIncomingNwkFrameCounterResponse : EzspFrameResponse
{
    public uint parentIncomingNwkFrameCounter { get; set; }

}
