using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPowerFrames.Structure;

/// <summary>
/// Update the GP Proxy table based on a GP pairing.
/// Frame value: 0x00C9
/// </summary>
public class gpProxyTableProcessGpPairingResponse : EzspFrameResponse
{
    /// <summary>
    /// Whether a GP Pairing has been created or not.
    /// </summary>
    public bool gpPairingAdded { get; set; }

}
