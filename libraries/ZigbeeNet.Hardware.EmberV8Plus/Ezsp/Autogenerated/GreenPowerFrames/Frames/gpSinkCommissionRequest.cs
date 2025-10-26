using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Command;

/// <summary>
/// Puts the GPS in commissioning mode.
/// Frame value: 0x010A
/// </summary>
public class gpSinkCommission : EzspFrameRequest
{
    /// <summary>
    /// commissioning options
    /// </summary>
    public byte options { get; set; }

    /// <summary>
    /// gpm address for security.
    /// </summary>
    public ushort gpmAddrForSecurity { get; set; }

    /// <summary>
    /// gpm address for pairing.
    /// </summary>
    public ushort gpmAddrForPairing { get; set; }

    /// <summary>
    /// sink endpoint.
    /// </summary>
    public byte sinkEndpoint { get; set; }

}
