#if VERSION_2025_6_2
/// <summary>
/// Information about a specific ZLL Device.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeZllDeviceInfoRecord
{
	/// <summary>
	/// EUI64 associated with the device.
	/// </summary>
	public 802154LongAddr ieeeAddress;

	/// <summary>
	/// Endpoint id.
	/// </summary>
	public byte endpointId;

	/// <summary>
	/// Profile id.
	/// </summary>
	public ushort profileId;

	/// <summary>
	/// Device id.
	/// </summary>
	public ushort deviceId;

	/// <summary>
	/// Associated version.
	/// </summary>
	public byte version;

	/// <summary>
	/// Number of relevant group ids.
	/// </summary>
	public byte groupIdCount;

}


#endif