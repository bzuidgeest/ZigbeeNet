#if VERSION_2025_6_2
/// <summary>
/// A neighbor table entry stores information about the reliability of RF links to and from neighboring nodes.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeNeighborTableEntry
{
	/// <summary>
	/// The neighbor&apos;s two byte network id
	/// </summary>
	public ushort shortId;

	/// <summary>
	/// An exponentially weighted moving average of the link quality values of incoming packets from this neighbor as reported by the PHY.
	/// </summary>
	public byte averageLqi;

	/// <summary>
	/// The incoming cost for this neighbor, computed from the average LQI. Values range from 1 for a good link to 7 for a bad link.
	/// </summary>
	public byte inCost;

	/// <summary>
	/// The outgoing cost for this neighbor, obtained from the most recently received neighbor exchange message from the neighbor. A value of zero means that a neighbor exchange message from the neighbor has not been received recently enough, or that our id was not present in the most recently received one.
	/// </summary>
	public byte outCost;

	/// <summary>
	/// The number of aging periods elapsed since a link status message was last received from this neighbor. The aging period is 16 seconds.
	/// </summary>
	public byte age;

	/// <summary>
	/// The 8 byte EUI64 of the neighbor.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] longId;

}


#endif