/// <summary>
/// A structure containing a key and its associated data.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeKeyStruct
{
	/// <summary>
	/// A bitmask indicating the presence of data within the various fields in the structure.
	/// </summary>
	public ZigbeeKeyStructBitmask bitmask;

	/// <summary>
	/// The type of the key.
	/// </summary>
	public ZigbeeKeyType type;

	/// <summary>
	/// The actual key data.
	/// </summary>
	public ZigbeeKeyData key;

	/// <summary>
	/// The outgoing frame counter associated with the key.
	/// </summary>
	public uint outgoingFrameCounter;

	/// <summary>
	/// The frame counter of the partner device associated with the key.
	/// </summary>
	public uint incomingFrameCounter;

	/// <summary>
	/// The sequence number associated with the key.
	/// </summary>
	public byte sequenceNumber;

	/// <summary>
	/// The IEEE address of the partner device also in possession of the key.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] partnerEUI64;

}

