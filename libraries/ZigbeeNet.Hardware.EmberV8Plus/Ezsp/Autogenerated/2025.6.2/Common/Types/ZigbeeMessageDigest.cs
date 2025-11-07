#if VERSION_2025_6_2
/// <summary>
/// The calculated digest of a message
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeMessageDigest
{
	/// <summary>
	/// The calculated digest of a message.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	public byte[] contents;
}


#endif