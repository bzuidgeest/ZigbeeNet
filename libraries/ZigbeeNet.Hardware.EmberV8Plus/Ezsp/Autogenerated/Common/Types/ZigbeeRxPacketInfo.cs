/// <summary>
/// Incoming message Information
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeRxPacketInfo
{
	/// <summary>
	/// Short ID of the sender of the message.
	/// </summary>
	public ushort sender_short_id;

	/// <summary>
	/// EUI64 of the sender of the message if the sender chose to this information in the message. The ::SL_ZIGBEE_APS_OPTION_SOURCE_EUI64 bit in the options field of the APS frame of the incoming message indicates that the EUI64 is present in the message.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] sender_long_id;

	/// <summary>
	/// The index of the entry in the binding table that matches the sender of the message or 0xFF if there is no matching entry.
	/// </summary>
	public byte binding_index;

	/// <summary>
	/// The index of the entry in the address table that matches the sender of the message or 0xFF if there is no matching entry.
	/// </summary>
	public byte address_index;

	/// <summary>
	/// Link quality of the node that last relayed the current message.
	/// </summary>
	public byte lasy_hop_lqi;

	/// <summary>
	/// Received signal strength indicator (RSSI) of the node that last relayed the message.
	/// </summary>
	public sbyte lasy_hop_rssi;

	/// <summary>
	/// Timestamp of the moment when Start Frame Delimiter (SFD) was received.
	/// </summary>
	public uint lasy_hop_timestamp;

}

