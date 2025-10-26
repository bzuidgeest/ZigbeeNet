using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenterFrames.Structure;

/// <summary>
/// This function broadcasts a new encryption key, but does not tell the nodes in the network to start using it. To tell nodes to switch to the new key, use sl_zigbee_send_network_key_switch(). This is only valid for the Trust Center/Coordinator. It is up to the application to determine how quickly to send the Switch Key after sending the alternate encryption key.
/// Frame value: 0x0073
/// </summary>
public class broadcastNextNetworkKeyResponse : EzspFrameResponse
{
    /// <summary>
    /// sl_status_t value that indicates the success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

}
