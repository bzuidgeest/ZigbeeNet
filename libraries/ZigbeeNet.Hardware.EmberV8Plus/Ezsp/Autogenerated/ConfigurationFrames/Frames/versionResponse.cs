using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Structure;

/// <summary>
/// The command allows the Host to specify the desired EZSP version and must be sent before any other command. The response provides information about the firmware running on the NCP.
/// Frame value: 0x0000
/// </summary>
public class versionResponse : EzspFrameResponse
{
    /// <summary>
    /// The EZSP version the NCP is using.
    /// </summary>
    public byte protocolVersion { get; set; }

    /// <summary>
    /// The type of stack running on the NCP (2).
    /// </summary>
    public byte stackType { get; set; }

    /// <summary>
    /// The version number of the stack.
    /// </summary>
    public ushort stackVersion { get; set; }

}
