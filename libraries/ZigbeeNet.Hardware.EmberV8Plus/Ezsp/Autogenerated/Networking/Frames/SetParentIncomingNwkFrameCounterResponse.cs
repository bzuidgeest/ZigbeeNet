using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

public class SetParentIncomingNwkFrameCounterResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
