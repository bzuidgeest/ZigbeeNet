using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Indicates that the NCP received an invalid command.
/// Frame value: 0x0058
/// </summary>
public class invalidCommandResponse : EzspFrameResponse
{
    /// <summary>
    /// The reason why the command was invalid.
    /// </summary>
    public sl_zigbee_ezsp_status_t reason { get; set; }

}
