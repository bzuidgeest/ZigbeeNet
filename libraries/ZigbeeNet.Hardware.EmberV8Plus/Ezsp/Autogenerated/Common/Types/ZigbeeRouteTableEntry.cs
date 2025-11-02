/// <summary>
/// A route table entry stores information about the next hop along the route to the destination.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeRouteTableEntry
{
	/// <summary>
	/// The short id of the destination. A value of 0xFFFF indicates the entry is unused.
	/// </summary>
	public ushort destination;

	/// <summary>
	/// The short id of the next hop to this destination.
	/// </summary>
	public ushort nextHop;

	/// <summary>
	/// Indicates whether this entry is active (0), being discovered (1), unused (3), or validating (4).
	/// </summary>
	public byte status;

	/// <summary>
	/// The number of seconds since this route entry was last used to send a packet.
	/// </summary>
	public byte age;

	/// <summary>
	/// Indicates whether this destination is a High RAM Concentrator (2), a Low RAM Concentrator (1), or not a concentrator (0).
	/// </summary>
	public byte concentratorType;

	/// <summary>
	/// For a High RAM Concentrator, indicates whether a route record is needed (2), has been sent (1), or is no long needed (0) because a source routed message from the concentrator has been received.
	/// </summary>
	public byte routeRecordState;

}

