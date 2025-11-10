#if VERSION_2025_6_2
/// <summary>
/// Context for Zigbee Security Manager operations.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeSecManContext
{
	/// <summary>
	/// The type of key being referenced.
	/// </summary>
	public ZigbeeSecManKeyType core_key_type;

	/// <summary>
	/// The index of the referenced key.
	/// </summary>
	public byte key_index;

	/// <summary>
	/// The type of key derivation operation to perform on a key.
	/// </summary>
	public ZigbeeSecManDerivedKeyType derived_type;

	/// <summary>
	/// The EUI64 associated with this key.
	/// </summary>
	public 802154LongAddr eui64;

	/// <summary>
	/// Multi-network index.
	/// </summary>
	public byte multi_network_index;

	/// <summary>
	/// Flag bitmask.
	/// </summary>
	public ZigbeeSecManFlags flags;

	/// <summary>
	/// Algorithm to use with this key (for PSA APIs)
	/// </summary>
	public uint psa_key_alg_permission;

}


#endif