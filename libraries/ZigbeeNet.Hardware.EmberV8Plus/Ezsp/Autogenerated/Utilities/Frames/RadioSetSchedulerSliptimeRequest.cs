using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Set the current multiprotocol sliptime
/// Frame value: 0x012D
/// </summary>
public class RadioSetSchedulerSliptimeRequest : EzspFrameRequest
{
    /// <summary>
    /// Value of the current slip time.
    /// </summary>
    public uint slipTime { get; set; }

