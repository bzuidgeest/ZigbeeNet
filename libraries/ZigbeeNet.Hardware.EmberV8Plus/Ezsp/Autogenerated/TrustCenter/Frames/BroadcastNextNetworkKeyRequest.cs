using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenter.Frames;

/// <summary>
/// This function broadcasts a new encryption key, but does not tell the nodes in the network to start using it. To tell nodes to switch to the new key, use sl_zigbee_send_network_key_switch(). This is only valid for the Trust Center/Coordinator. It is up to the application to determine how quickly to send the Switch Key after sending the alternate encryption key.
/// Frame value: 0x0073
/// </summary>
public class BroadcastNextNetworkKeyRequest : EzspFrameRequest
{
    /// <summary>
    /// An optional pointer to a 16-byte encryption key (SL_ZIGBEE_ENCRYPTION_KEY_SIZE). An all zero key may be passed in, which will cause the stack to randomly generate a new key.
    /// </summary>
    public sl_zigbee_key_data_t key { get; set; }

}
