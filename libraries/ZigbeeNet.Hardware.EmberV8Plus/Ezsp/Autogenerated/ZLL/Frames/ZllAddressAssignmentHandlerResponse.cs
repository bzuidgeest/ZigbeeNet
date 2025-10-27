using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// This call is fired when network and group addresses are assigned to a remote mode in a network start or network join request.
/// Frame value: 0x00B8
/// </summary>
public class ZllAddressAssignmentHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// Address assignment information.
    /// </summary>
    public sl_zigbee_zll_address_assignment_t addressInfo { get; set; }

    /// <summary>
    /// Information about the incoming packet.
    /// </summary>
    public sl_zigbee_rx_packet_info_t packetInfo { get; set; }

}
