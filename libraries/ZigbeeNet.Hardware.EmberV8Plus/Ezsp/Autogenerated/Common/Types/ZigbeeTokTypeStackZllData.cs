/// <summary>
/// Public API for ZLL stack data token.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeTokTypeStackZllData
{
	/// <summary>
	/// Token bitmask.
	/// </summary>
	public uint bitmask;

	/// <summary>
	/// Minimum free node id.
	/// </summary>
	public ushort freeNodeIdMin;

	/// <summary>
	/// Maximum free node id.
	/// </summary>
	public ushort freeNodeIdMax;

	/// <summary>
	/// Local minimum group id.
	/// </summary>
	public ushort myGroupIdMin;

	/// <summary>
	/// Minimum free group id.
	/// </summary>
	public ushort freeGroupIdMin;

	/// <summary>
	/// Maximum free group id.
	/// </summary>
	public ushort freeGroupIdMax;

	/// <summary>
	/// RSSI correction value.
	/// </summary>
	public byte rssiCorrection;

}

