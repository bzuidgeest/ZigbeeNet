#if VERSION_2025_6_2
/// <summary>
/// A sink list entry.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeGpSinkListEntry
{
	/// <summary>
	/// The sink list type.
	/// </summary>
	public byte type;

	/// <summary>
	/// The EUI64 of the target sink.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] sinkEUI;

	/// <summary>
	/// The short address of the target sink.
	/// </summary>
	public ushort sinkNodeId;

}


#endif