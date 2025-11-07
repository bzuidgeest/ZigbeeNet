#if VERSION_2025_6_2
/// <summary>
/// The Shared Message Authentication Code data used in CBKE.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeSmacData
{
	/// <summary>
	/// The Shared Message Authentication Code data.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	public byte[] contents;
}


#endif