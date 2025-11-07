#if VERSION_2025_6_2
/// <summary>
/// The security options and information currently used by the stack.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeCurrentSecurityState
{
	/// <summary>
	/// A bitmask indicating the security options currently in use by a device joined in the network.
	/// </summary>
	public ZigbeeCurrentSecurityBitmask bitmask;

	/// <summary>
	/// The IEEE Address of the Trust Center device.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] trustCenterLongAddress;

}


#endif