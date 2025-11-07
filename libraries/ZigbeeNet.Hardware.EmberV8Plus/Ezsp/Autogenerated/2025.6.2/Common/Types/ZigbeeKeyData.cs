#if VERSION_2025_6_2
/// <summary>
/// A 128-bit key.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeKeyData
{
	/// <summary>
	/// The key data.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	public byte[] contents;
}


#endif