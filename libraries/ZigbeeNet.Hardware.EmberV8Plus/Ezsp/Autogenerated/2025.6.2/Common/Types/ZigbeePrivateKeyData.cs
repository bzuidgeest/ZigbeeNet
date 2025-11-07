#if VERSION_2025_6_2
/// <summary>
/// The private key data used in CBKE.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeePrivateKeyData
{
	/// <summary>
	/// The private key data.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
	public byte[] contents;
}


#endif