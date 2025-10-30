using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Returns the current radio parameters based on phy index.
/// Frame value: 0x00FD
/// </summary>
public class GetRadioParametersRequest : EzspFrameRequest
{
    /// <summary>
    /// Desired index of phy interface for radio parameters.
    /// </summary>
    public byte phyIndex { get; set; }

