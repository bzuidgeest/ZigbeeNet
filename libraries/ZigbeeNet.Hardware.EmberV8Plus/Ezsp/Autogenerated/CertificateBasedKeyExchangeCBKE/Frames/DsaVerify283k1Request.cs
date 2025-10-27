using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// Verify that signature of the associated message digest was signed by the private key of the associated certificate.
/// Frame value: 0x00B0
/// </summary>
public class DsaVerify283k1Request : EzspFrameRequest
{
    /// <summary>
    /// The AES-MMO message digest of the signed data. If dsaSign command was used to generate the signature for this data, the final byte (replaced by signature type of 0x01) in the messageContents array passed to dsaSign is included in the hash context used for the digest calculation.
    /// </summary>
    public sl_zigbee_message_digest_t digest { get; set; }

    /// <summary>
    /// The certificate of the signer. Note that the signer&apos;s certificate and the verifier&apos;s certificate must both be issued by the same Certificate Authority, so they should share the same CA Public Key.
    /// </summary>
    public sl_zigbee_certificate_283k1_data_t signerCertificate { get; set; }

    /// <summary>
    /// The signature of the signed data.
    /// </summary>
    public sl_zigbee_signature_283k1_data_t receivedSig { get; set; }

}
