using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

public class setParentIncomingNwkFrameCounterResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
