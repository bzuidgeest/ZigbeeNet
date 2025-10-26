using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Command;

/// <summary>
/// Allows the Host to control the broadcast behaviour of a routing device used by the NCP.
/// Frame value: 0x0105
/// </summary>
public class setPassiveAckConfig : EzspFrameRequest
{
    /// <summary>
    /// Passive ack config enum.
    /// </summary>
    public byte config { get; set; }

    /// <summary>
    /// The minimum number of acknowledgments (re-broadcasts) to wait for until deeming the broadcast transmission complete.
    /// </summary>
    public byte minAcksNeeded { get; set; }

}
