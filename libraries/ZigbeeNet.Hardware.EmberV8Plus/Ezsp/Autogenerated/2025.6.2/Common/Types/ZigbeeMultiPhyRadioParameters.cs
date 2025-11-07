#if VERSION_2025_6_2
/// <summary>
/// Radio parameters.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeMultiPhyRadioParameters
{
	/// <summary>
	/// A power setting, in dBm.
	/// </summary>
	public sbyte radioTxPower;

	/// <summary>
	/// A radio page.
	/// </summary>
	public byte radioPage;

	/// <summary>
	/// A radio channel.
	/// </summary>
	public byte radioChannel;

}


#endif