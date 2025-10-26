using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// This is a callback that indicates the success or failure of an attempt to establish a key with a partner device.
/// Frame value: 0x009B
/// </summary>
public class zigbeeKeyEstablishmentHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// This is the IEEE address of the partner that the device successfully established a key with. This value is all zeros on a failure.
    /// </summary>
    public sl_802154_long_addr_t partner { get; set; }

    /// <summary>
    /// This is the status indicating what was established or why the key establishment failed.
    /// </summary>
    public sl_zigbee_key_status_t status { get; set; }

}
