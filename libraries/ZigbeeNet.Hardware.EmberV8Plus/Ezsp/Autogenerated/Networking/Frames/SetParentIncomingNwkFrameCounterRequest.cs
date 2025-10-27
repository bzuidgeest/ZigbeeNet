using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

public class SetParentIncomingNwkFrameCounterRequest : EzspFrameRequest
{
    public uint value { get; set; }

}
