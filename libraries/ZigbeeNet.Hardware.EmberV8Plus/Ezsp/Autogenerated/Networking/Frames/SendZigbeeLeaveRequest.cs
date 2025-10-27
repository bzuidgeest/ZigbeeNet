using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Send a Zigbee NWK Leave command to the destination.
/// Frame value: 0x011A
/// </summary>
public class SendZigbeeLeaveRequest : EzspFrameRequest
{
    /// <summary>
    /// Node ID of the device being told to leave.
    /// </summary>
    public sl_802154_pan_id_t destination { get; set; }

    /// <summary>
    /// Bitmask indicating additional considerations for the leave request.
    /// </summary>
    public sl_zigbee_leave_request_flags_t flags { get; set; }

}
