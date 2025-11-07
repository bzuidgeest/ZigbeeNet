#if VERSION_2025_6_2
/// <summary>
/// The hash context for an ongoing hash operation.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeAesMmoHashContext
{
	/// <summary>
	/// The result of ongoing the hash operation.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	public byte[] result;
	/// <summary>
	/// The total length of the data that has been hashed so far.
	/// </summary>
	public uint length;

}


#endif