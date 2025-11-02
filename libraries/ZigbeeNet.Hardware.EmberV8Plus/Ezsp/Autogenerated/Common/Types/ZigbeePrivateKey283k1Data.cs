/// <summary>
/// The private key data used in CBKE.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeePrivateKey283k1Data
{
	/// <summary>
	/// The 283k1 private key data.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
	public byte[] contents;
}

