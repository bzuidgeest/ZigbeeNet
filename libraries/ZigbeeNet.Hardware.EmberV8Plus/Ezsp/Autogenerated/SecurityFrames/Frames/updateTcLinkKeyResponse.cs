using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// Requests a new link key from the Trust Center. This function starts by sending a Node Descriptor request to the Trust Center to verify its R21+ stack version compliance. A Request Key message will then be sent, followed by a Verify Key Confirm message.
/// Frame value: 0x006C
/// </summary>
public class updateTcLinkKeyResponse : EzspFrameResponse
{
    /// <summary>
    /// The success or failure of sending the request. If the Node Descriptor is successfully transmitted, sl_zigbee_ezsp_zigbee_key_establishment_handler(...) will be called at a later time with a final status result.
    /// </summary>
    public sl_status_t status { get; set; }

}
