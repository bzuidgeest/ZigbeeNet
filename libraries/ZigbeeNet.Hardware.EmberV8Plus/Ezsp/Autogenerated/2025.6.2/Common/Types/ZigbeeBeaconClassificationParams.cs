#if VERSION_2025_6_2
/// <summary>
/// The parameters related to beacon prioritization.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeBeaconClassificationParams
{
	/// <summary>
	/// The min rssi value for receiving packets that is used is some beacon prioritization algorithms.
	/// </summary>
	public sbyte minRssiForReceivingPkts;

	/// <summary>
	/// The beacon classification mask that identifies which beacon prioritization algorithm to pick, and defines the relevant parameters.
	/// </summary>
	public ushort beaconClassificationMask;

}


#endif