namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// An ECDSA signature
/// </summary>
public struct sl_zigbee_signature_283k1_data_t
{
    /// <summary>
    /// The 283k1 signature data.
    /// </summary>
    public fixed byte contents[72];

}

