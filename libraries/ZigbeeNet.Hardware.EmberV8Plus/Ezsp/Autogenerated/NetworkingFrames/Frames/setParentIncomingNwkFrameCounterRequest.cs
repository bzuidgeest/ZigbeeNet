using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

public class setParentIncomingNwkFrameCounter : EzspFrameRequest
{
    public uint value { get; set; }

}
