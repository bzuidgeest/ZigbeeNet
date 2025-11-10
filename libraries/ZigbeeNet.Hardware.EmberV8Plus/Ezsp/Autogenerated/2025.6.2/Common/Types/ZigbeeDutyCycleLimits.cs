#if VERSION_2025_6_2
/// <summary>
/// A structure containing duty cycle limit configurations. All limits are absolute, and are required to be as follows: suspLimit &gt; critThresh &gt; limitThresh For example:  suspLimit = 250 (2.5%), critThresh = 180 (1.8%), limitThresh 100 (1.00%).
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeDutyCycleLimits
{
	/// <summary>
	/// The Limited Threshold in % * 100
	/// </summary>
	public ZigbeeDutyCycleHectoPct limitThresh;

	/// <summary>
	/// The Critical Threshold in % * 100.
	/// </summary>
	public ZigbeeDutyCycleHectoPct critThresh;

	/// <summary>
	/// The Suspended Limit (LBT) in % * 100.
	/// </summary>
	public ZigbeeDutyCycleHectoPct suspLimit;

}


#endif