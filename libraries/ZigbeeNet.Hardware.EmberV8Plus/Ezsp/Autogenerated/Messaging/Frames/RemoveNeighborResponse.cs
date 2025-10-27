using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Remove a neighbor from neighbor table only on SoC, allowing direct manipulation of neighbor table by the application. This can affect the network functionality, and needs to be used wisely.
/// Frame value: 0x013A
/// </summary>
public class RemoveNeighborResponse : EzspFrameResponse
{
}
