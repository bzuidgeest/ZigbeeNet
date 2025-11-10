#if VERSION_2025_6_2
/// <summary>
/// A structure containing per device overall duty cycle consumed (up to the suspend limit).
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeePerDeviceDutyCycle
{
	/// <summary>
	/// Node Id of device whose duty cycle is reported.
	/// </summary>
	public 802154ShortAddr nodeId;

	/// <summary>
	/// Amount of overall duty cycle consumed (up to suspend limit).
	/// </summary>
	public ZigbeeDutyCycleHectoPct dutyCycleConsumed;

}


#endif