using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Update the GP Proxy table based on a GP pairing.
/// Frame value: 0x00C9
/// </summary>
public class GpProxyTableProcessGpPairingResponse : EzspFrameResponse
{
    /// <summary>
    /// Whether a GP Pairing has been created or not.
    /// </summary>
    public bool gpPairingAdded { get; set; }

}
