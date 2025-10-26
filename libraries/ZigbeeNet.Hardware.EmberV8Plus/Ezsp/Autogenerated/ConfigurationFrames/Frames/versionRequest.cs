using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Command;

/// <summary>
/// The command allows the Host to specify the desired EZSP version and must be sent before any other command. The response provides information about the firmware running on the NCP.
/// Frame value: 0x0000
/// </summary>
public class version : EzspFrameRequest
{
    /// <summary>
    /// The EZSP version the Host wishes to use. To successfully set the version and allow other commands, this must be same as EZSP_PROTOCOL_VERSION.
    /// </summary>
    public byte desiredProtocolVersion { get; set; }

}
