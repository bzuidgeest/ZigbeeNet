#if VERSION_2025_6_2
/// <summary>
/// The internal representation of a sink table entry.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeGpSinkTableEntry
{
	/// <summary>
	/// Internal status of the sink table entry.
	/// </summary>
	public byte status;

	/// <summary>
	/// The tunneling options (this contains both options and extendedOptions from the spec).
	/// </summary>
	public uint options;

	/// <summary>
	/// The addressing info of the GPD.
	/// </summary>
	public ZigbeeGpAddress gpd;

	/// <summary>
	/// The device id for the GPD.
	/// </summary>
	public byte deviceId;

	/// <summary>
	/// The list of sinks (hardcoded to 2 which is the spec minimum).
	/// </summary>
	// Array field with symbolic size: GP_SINK_LIST_ENTRIES
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
	public ZigbeeGpSinkListEntry[] sinkList;
	/// <summary>
	/// The assigned alias for the GPD.
	/// </summary>
	public ushort assignedAlias;

	/// <summary>
	/// The groupcast radius.
	/// </summary>
	public byte groupcastRadius;

	/// <summary>
	/// The security options field.
	/// </summary>
	public byte securityOptions;

	/// <summary>
	/// The security frame counter of the GPD.
	/// </summary>
	public uint gpdSecurityFrameCounter;

	/// <summary>
	/// The key to use for GPD.
	/// </summary>
	public ZigbeeKeyData gpdKey;

}


#endif