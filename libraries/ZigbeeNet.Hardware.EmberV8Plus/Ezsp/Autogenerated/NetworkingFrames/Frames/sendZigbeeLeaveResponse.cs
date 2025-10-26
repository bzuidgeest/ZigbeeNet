using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Send a Zigbee NWK Leave command to the destination.
/// Frame value: 0x011A
/// </summary>
public class sendZigbeeLeaveResponse : EzspFrameResponse
{
    /// <summary>
    /// Status indicating success or a reason for failure. Call is invalid if destination is on network or is the local node.
    /// </summary>
    public sl_status_t status { get; set; }

}
