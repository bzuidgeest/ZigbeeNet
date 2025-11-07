#if VERSION_2025_6_2
/// <summary>
/// Defines alternate MAC configuration parameters.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeAltMacConfig
{
	/// <summary>
	/// Scan duration over alternate MAC.
	/// </summary>
	public ushort scanDuration;

	/// <summary>
	/// To register the transmit callback. Called when there is packet to transmit.
	/// </summary>
	public Mactransmitcallback macTransmit;

}


#endif