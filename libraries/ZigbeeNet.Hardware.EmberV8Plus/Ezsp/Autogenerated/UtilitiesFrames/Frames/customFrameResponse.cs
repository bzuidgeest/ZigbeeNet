using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Provides the customer a custom EZSP frame. On the NCP, these frames are only handled if the XNCP library is included. On the NCP side these frames are handled in the sl_zigbee_xncp_incoming_custom_ezsp_message_cb() callback function.
/// Frame value: 0x0047
/// </summary>
public class customFrameResponse : EzspFrameResponse
{
    /// <summary>
    /// The status returned by the custom command.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The length of the response.
    /// </summary>
    public byte replyLength { get; set; }

    /// <summary>
    /// The response.
    /// </summary>
    public uint8_t[replyLength] reply { get; set; }

}
