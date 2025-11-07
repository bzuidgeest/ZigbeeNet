#if VERSION_2025_6_2
/// <summary>
/// A GP address structure.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeGpAddress
{
	/// <summary>
	/// Contains either a 4-byte source ID or an 8-byte IEEE address, as indicated by the value of the applicationId field.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] id;
	/// <summary>
	/// The GPD Application ID specifying either source ID (0x00) or IEEE address (0x02).
	/// </summary>
	public byte applicationId;

	/// <summary>
	/// The GPD endpoint.
	/// </summary>
	public byte endpoint;

}


#endif