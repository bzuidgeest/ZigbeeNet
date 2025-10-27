using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Set initial outgoing link cost for neighbor.
/// Frame value: 0x0122
/// </summary>
public class SetInitialNeighborOutgoingCostResponse : EzspFrameResponse
{
    /// <summary>
    /// Whether or not initial cost was successfully set.
    /// </summary>
    public sl_status_t status { get; set; }

}
