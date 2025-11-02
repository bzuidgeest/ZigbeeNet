/// <summary>
/// The internal representation of a proxy table entry.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeGpProxyTableEntry
{
	/// <summary>
	/// Internal status of the proxy table entry.
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
	/// The assigned alias for the GPD.
	/// </summary>
	public ushort assignedAlias;

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

	/// <summary>
	/// The list of sinks (hardcoded to 2 which is the spec minimum).
	/// </summary>
	// Array field with symbolic size: GP_SINK_LIST_ENTRIES
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = GP_SINK_LIST_ENTRIES)]
	public ZigbeeGpSinkListEntry[] sinkList[GP_SINK_LIST_ENTRIES];
	/// <summary>
	/// The groupcast radius.
	/// </summary>
	public byte groupcastRadius;

	/// <summary>
	/// The search counter.
	/// </summary>
	public byte searchCounter;

}

