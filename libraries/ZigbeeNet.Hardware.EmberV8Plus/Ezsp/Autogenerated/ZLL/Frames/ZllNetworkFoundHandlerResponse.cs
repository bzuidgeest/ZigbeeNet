using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// This call is fired when a ZLL network scan finds a ZLL network.
/// Frame value: 0x00B6
/// </summary>
public class ZllNetworkFoundHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// Information about the network.
    /// </summary>
    public sl_zigbee_zll_network_t networkInfo { get; set; }

    /// <summary>
    /// Used to interpret deviceInfo field.
    /// </summary>
    public bool isDeviceInfoNull { get; set; }

    /// <summary>
    /// Device specific information.
    /// </summary>
    public sl_zigbee_zll_device_info_record_t deviceInfo { get; set; }

    /// <summary>
    /// Information about the incoming packet received from this network.
    /// </summary>
    public sl_zigbee_rx_packet_info_t packetInfo { get; set; }

}
