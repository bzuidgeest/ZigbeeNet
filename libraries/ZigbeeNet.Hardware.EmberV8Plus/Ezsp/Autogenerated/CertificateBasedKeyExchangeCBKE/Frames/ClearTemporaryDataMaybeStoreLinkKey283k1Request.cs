using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// Clears the temporary data associated with CBKE and the key establishment, most notably the ephemeral public/private key pair. If storeLinKey is true it moves the unverified link key stored in temporary storage into the link key table. Otherwise it discards the key.
/// Frame value: 0x00EE
/// </summary>
public class ClearTemporaryDataMaybeStoreLinkKey283k1Request : EzspFrameRequest
{
    /// <summary>
    /// A bool indicating whether to store (true) or discard (false) the unverified link key derived when sl_zigbee_ezsp_calculate_smacs() was previously called.
    /// </summary>
    public bool storeLinkKey { get; set; }

}
