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
	public ushort nodeId;

	/// <summary>
	/// Minimum free node id.
	/// </summary>
	public ushort freeNodeIdMin;

	/// <summary>
	/// Maximum free node id.
	/// </summary>
	public ushort freeNodeIdMax;

	/// <summary>
	/// Minimum group id.
	/// </summary>
	public ushort groupIdMin;

	/// <summary>
	/// Maximum group id.
	/// </summary>
	public ushort groupIdMax;

	/// <summary>
	/// Minimum free group id.
	/// </summary>
	public ushort freeGroupIdMin;

	/// <summary>
	/// Maximum free group id.
	/// </summary>
	public ushort freeGroupIdMax;

}


#endif