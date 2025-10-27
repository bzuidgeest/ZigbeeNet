using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

public class GetParentIncomingNwkFrameCounterResponse : EzspFrameResponse
{
    public uint parentIncomingNwkFrameCounter { get; set; }

}
