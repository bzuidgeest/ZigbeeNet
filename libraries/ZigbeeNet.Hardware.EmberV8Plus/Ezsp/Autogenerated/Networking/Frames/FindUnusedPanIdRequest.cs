using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// This function starts a series of scans which will return an available panId.
/// Frame value: 0x00D3
/// </summary>
public class FindUnusedPanIdRequest : EzspFrameRequest
{
    /// <summary>
    /// The channels that will be scanned for available panIds.
    /// </summary>
    public uint channelMask { get; set; }

    /// <summary>
    /// The duration of the procedure.
    /// </summary>
    public byte duration { get; set; }

}
