/// <summary>
/// A structure containing a child node&apos;s data.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeChildData
{
	/// <summary>
	/// The EUI64 of the child
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] eui64;

	/// <summary>
	/// The node type of the child
	/// </summary>
	public ZigbeeNodeType type;

	/// <summary>
	/// The short address of the child
	/// </summary>
	public ushort id;

	/// <summary>
	/// The phy of the child
	/// </summary>
	public byte phy;

	/// <summary>
	/// The power of the child
	/// </summary>
	public byte power;

	/// <summary>
	/// The timeout of the child
	/// </summary>
	public byte timeout;

}

