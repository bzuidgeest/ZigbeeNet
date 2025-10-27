using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Get the current network.
/// Frame value: 0x014E
/// </summary>
public class GetCurrentNetworkResponse : EzspFrameResponse
{
    /// <summary>
    /// Return the current network index.
    /// </summary>
    public byte index { get; set; }

}
