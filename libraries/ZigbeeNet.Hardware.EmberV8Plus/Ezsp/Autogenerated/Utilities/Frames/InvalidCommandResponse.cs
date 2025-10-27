using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Indicates that the NCP received an invalid command.
/// Frame value: 0x0058
/// </summary>
public class InvalidCommandResponse : EzspFrameResponse
{
    /// <summary>
    /// The reason why the command was invalid.
    /// </summary>
    public sl_zigbee_ezsp_status_t reason { get; set; }

}
