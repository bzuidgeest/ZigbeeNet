/// <summary>
/// An ECDSA signature
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeSignatureData
{
	/// <summary>
	/// The signature data.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 42)]
	public byte[] contents;
}

