using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKEFrames.Structure;

/// <summary>
/// Clears the temporary data associated with CBKE and the key establishment, most notably the ephemeral public/private key pair. If storeLinKey is true it moves the unverified link key stored in temporary storage into the link key table. Otherwise it discards the key.
/// Frame value: 0x00A1
/// </summary>
public class clearTemporaryDataMaybeStoreLinkKeyResponse : EzspFrameResponse
{
    public sl_status_t status { get; set; }

}
