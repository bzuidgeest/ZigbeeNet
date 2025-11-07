#if VERSION_2025_6_2
/// <summary>
/// Beacon data structure.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeBeaconData
{
	/// <summary>
	/// The channel of the received beacon.
	/// </summary>
	public byte channel;

	/// <summary>
	/// The LQI of the received beacon.
	/// </summary>
	public byte lqi;

	/// <summary>
	/// The RSSI of the received beacon.
	/// </summary>
	public sbyte rssi;

	/// <summary>
	/// The depth of the received beacon.
	/// </summary>
	public byte depth;

	/// <summary>
	/// The network update ID of the received beacon.
	/// </summary>
	public byte nwkUpdateId;

	/// <summary>
	/// The power level of the received beacon. This field is valid only if the beacon is an enhanced beacon.
	/// </summary>
	public sbyte power;

	/// <summary>
	/// The TC connectivity and long uptime from capacity field.
	/// </summary>
	public sbyte parentPriority;

	/// <summary>
	/// The PAN ID of the received beacon.
	/// </summary>
	public ushort panId;

	/// <summary>
	/// The extended PAN ID of the received beacon.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] extendedPanId;
	/// <summary>
	/// The sender of the received beacon.
	/// </summary>
	public ushort sender;

	/// <summary>
	/// Whether or not the beacon is enhanced.
	/// </summary>
	public bool enhanced;

	/// <summary>
	/// Whether the beacon is advertising permit join.
	/// </summary>
	public bool permitJoin;

	/// <summary>
	/// Whether the beacon is advertising capacity.
	/// </summary>
	public bool hasCapacity;

}


#endif