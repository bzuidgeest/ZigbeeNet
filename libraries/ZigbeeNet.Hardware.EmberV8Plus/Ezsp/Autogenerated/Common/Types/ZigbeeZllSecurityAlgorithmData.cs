/// <summary>
/// Data associated with the ZLL security algorithm.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeZllSecurityAlgorithmData
{
	/// <summary>
	/// Transaction identifier.
	/// </summary>
	public uint transactionId;

	/// <summary>
	/// Response identifier.
	/// </summary>
	public uint responseId;

	/// <summary>
	/// Bitmask.
	/// </summary>
	public ushort bitmask;

}

