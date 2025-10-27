using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.CertificateBasedKeyExchangeCBKE.Frames;

/// <summary>
/// A callback to indicate that the NCP has finished calculating the Secure Message Authentication Codes (SMAC) for both the initiator and responder for the CBKE 283k1 Library. The associated link key is kept in temporary storage until the host tells the NCP to store or discard the key via sli_zigbee_stack_clear_temporary_data_maybe_store_link_key().
/// Frame value: 0x00EB
/// </summary>
public class CalculateSmacs283k1HandlerRequest : EzspFrameRequest
{
}
