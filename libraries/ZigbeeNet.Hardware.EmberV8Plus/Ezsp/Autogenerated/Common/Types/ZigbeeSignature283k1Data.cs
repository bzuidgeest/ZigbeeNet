/// <summary>
/// An ECDSA signature
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeSignature283k1Data
{
	/// <summary>
	/// The 283k1 signature data.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 72)]
	public byte[] contents;
}

