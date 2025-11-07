#if VERSION_2025_6_2
/// <summary>
/// The implicit certificate used in CBKE.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeCertificateData
{
	/// <summary>
	/// The certificate data.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
	public byte[] contents;
}


#endif