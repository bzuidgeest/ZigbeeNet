/// <summary>
/// Token Data
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TokenInterface.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeTokenData
{
	/// <summary>
	/// Token data size in bytes
	/// </summary>
	public uint size;

	/// <summary>
	/// Token data pointer
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
	public byte[] data;
}

