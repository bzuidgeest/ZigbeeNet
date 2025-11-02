/// <summary>
/// Metadata for APS link keys.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeSecManApsKeyMetadata
{
	/// <summary>
	/// Bitmask of key properties
	/// </summary>
	public ZigbeeKeyStructBitmask bitmask;

	/// <summary>
	/// Outgoing frame counter.
	/// </summary>
	public uint outgoing_frame_counter;

	/// <summary>
	/// Incoming frame counter.
	/// </summary>
	public uint incoming_frame_counter;

	/// <summary>
	/// Remaining lifetime (for transient keys).
	/// </summary>
	public ushort ttl_in_seconds;

}

