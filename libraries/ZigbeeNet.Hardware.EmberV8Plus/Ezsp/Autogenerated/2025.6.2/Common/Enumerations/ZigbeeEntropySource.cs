#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Entropy sources.
/// </summary>
public enum ZigbeeEntropySource : byte
{
	/// <summary>
	/// Entropy source error.
	/// </summary>
    SL_ZIGBEE_ENTROPY_SOURCE_ERROR = 0,
	/// <summary>
	/// Entropy source is the radio.
	/// </summary>
    SL_ZIGBEE_ENTROPY_SOURCE_RADIO = 1,
	/// <summary>
	/// Entropy source is the TRNG powered by mbed TLS.
	/// </summary>
    SL_ZIGBEE_ENTROPY_SOURCE_MBEDTLS_TRNG = 2,
	/// <summary>
	/// Entropy source is powered by mbed TLS, the source is not TRNG.
	/// </summary>
    SL_ZIGBEE_ENTROPY_SOURCE_MBEDTLS = 3
}

#endif