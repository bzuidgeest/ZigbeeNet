#if VERSION_2025_6_2
/// <summary>
/// The parameters of a ZLL network.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeZllNetwork
{
	/// <summary>
	/// The parameters of a ZigBee network.
	/// </summary>
	public ZigbeeZigbeeNetwork zigbeeNetwork;

	/// <summary>
	/// Data associated with the ZLL security algorithm.
	/// </summary>
	public ZigbeeZllSecurityAlgorithmData securityAlgorithm;

	/// <summary>
	/// Associated EUI64.
	/// </summary>
	public 802154LongAddr eui64;

	/// <summary>
	/// The node id.
	/// </summary>
	public 802154ShortAddr nodeId;

	/// <summary>
	/// The ZLL state.
	/// </summary>
	public ZigbeeZllState state;

	/// <summary>
	/// The node type.
	/// </summary>
	public ZigbeeNodeType nodeType;

	/// <summary>
	/// The number of sub devices.
	/// </summary>
	public byte numberSubDevices;

	/// <summary>
	/// The total number of group identifiers.
	/// </summary>
	public byte totalGroupIdentifiers;

	/// <summary>
	/// RSSI correction value.
	/// </summary>
	public byte rssiCorrection;

}


#endif