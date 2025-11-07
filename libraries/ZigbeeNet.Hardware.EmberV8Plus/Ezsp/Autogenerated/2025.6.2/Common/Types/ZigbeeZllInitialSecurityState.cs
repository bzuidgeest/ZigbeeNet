#if VERSION_2025_6_2
/// <summary>
/// Describes the initial security features and requirements that will be used when forming or joining ZLL networks.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeZllInitialSecurityState
{
	/// <summary>
	/// Unused bitmask; reserved for future use.
	/// </summary>
	public uint bitmask;

	/// <summary>
	/// The key encryption algorithm advertised by the application.
	/// </summary>
	public ZigbeeZllKeyIndex keyIndex;

	/// <summary>
	/// The encryption key for use by algorithms that require it.
	/// </summary>
	public ZigbeeKeyData encryptionKey;

	/// <summary>
	/// The pre-configured link key used during classical ZigBee commissioning.
	/// </summary>
	public ZigbeeKeyData preconfiguredKey;

}


#endif