namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// An ECDSA signature
/// </summary>
public struct sl_zigbee_signature_data_t
{
    /// <summary>
    /// The signature data.
    /// </summary>
    public fixed byte contents[42];

}

