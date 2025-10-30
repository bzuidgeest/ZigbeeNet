using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Retrieves the sink table entry stored at the passed index.
/// Frame value: 0x00DD
/// </summary>
public class GpSinkTableGetEntryRequest : EzspFrameRequest
{
    /// <summary>
    /// The index of the requested sink table entry.
    /// </summary>
    public byte sinkIndex { get; set; }

