using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Resume network operation after a reboot. The node retains its original type. This should be called on startup whether or not the node was previously part of a network. SL_STATUS_NOT_JOINED is returned if the node is not part of a network. This command accepts options to control the network initialization.
/// Frame value: 0x0017
/// </summary>
public class NetworkInitRequest : EzspFrameRequest
{
    /// <summary>
    /// An sl_zigbee_network_init_struct_t containing the options for initialization.
    /// </summary>
    public sl_zigbee_network_init_struct_t networkInitStruct { get; set; }

