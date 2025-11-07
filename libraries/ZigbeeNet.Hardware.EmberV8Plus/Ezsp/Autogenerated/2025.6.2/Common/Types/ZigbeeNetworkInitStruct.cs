#if VERSION_2025_6_2
/// <summary>
/// Network Initialization parameters.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeNetworkInitStruct
{
	/// <summary>
	/// Configuration options for network init.
	/// </summary>
	public ZigbeeNetworkInitBitmask bitmask;

}


#endif