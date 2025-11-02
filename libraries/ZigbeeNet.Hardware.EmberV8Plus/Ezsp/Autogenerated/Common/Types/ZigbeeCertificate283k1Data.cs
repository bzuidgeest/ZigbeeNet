/// <summary>
/// The implicit certificate used in CBKE.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeCertificate283k1Data
{
	/// <summary>
	/// The 283k1 certificate data.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 74)]
	public byte[] contents;
}

