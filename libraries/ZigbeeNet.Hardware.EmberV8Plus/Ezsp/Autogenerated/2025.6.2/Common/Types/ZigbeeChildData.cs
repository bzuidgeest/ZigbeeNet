#if VERSION_2025_6_2
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
	public 802154LongAddr eui64;

	/// <summary>
	/// The node type of the child
	/// </summary>
	public ZigbeeNodeType type;

	/// <summary>
	/// The short address of the child
	/// </summary>
	public 802154ShortAddr id;

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


#endif