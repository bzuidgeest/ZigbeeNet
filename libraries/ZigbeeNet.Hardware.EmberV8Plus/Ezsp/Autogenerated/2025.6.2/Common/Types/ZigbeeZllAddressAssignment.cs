#if VERSION_2025_6_2
/// <summary>
/// ZLL address assignment data.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeZllAddressAssignment
{
	/// <summary>
	/// Relevant node id.
	/// </summary>
	public 802154ShortAddr nodeId;

	/// <summary>
	/// Minimum free node id.
	/// </summary>
	public 802154ShortAddr freeNodeIdMin;

	/// <summary>
	/// Maximum free node id.
	/// </summary>
	public 802154ShortAddr freeNodeIdMax;

	/// <summary>
	/// Minimum group id.
	/// </summary>
	public ZigbeeMulticastId groupIdMin;

	/// <summary>
	/// Maximum group id.
	/// </summary>
	public ZigbeeMulticastId groupIdMax;

	/// <summary>
	/// Minimum free group id.
	/// </summary>
	public ZigbeeMulticastId freeGroupIdMin;

	/// <summary>
	/// Maximum free group id.
	/// </summary>
	public ZigbeeMulticastId freeGroupIdMax;

}


#endif