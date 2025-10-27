using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns the current network parameters.
/// Frame value: 0x0028
/// </summary>
public class GetNetworkParametersRequest : EzspFrameRequest
{
}
