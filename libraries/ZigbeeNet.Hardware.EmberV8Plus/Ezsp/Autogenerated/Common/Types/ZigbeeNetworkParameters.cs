/// <summary>
/// Network parameters.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeNetworkParameters
{
	/// <summary>
	/// The network&apos;s extended PAN identifier.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] extendedPanId;
	/// <summary>
	/// The network&apos;s PAN identifier.
	/// </summary>
	public ushort panId;

	/// <summary>
	/// A power setting, in dBm.
	/// </summary>
	public sbyte radioTxPower;

	/// <summary>
	/// A radio channel.
	/// </summary>
	public byte radioChannel;

	/// <summary>
	/// The method used to initially join the network.
	/// </summary>
	public ZigbeeJoinMethod joinMethod;

	/// <summary>
	/// NWK Manager ID. The ID of the network manager in the current network. This may only be set at joining when using SL_ZIGBEE_USE_CONFIGURED_NWK_STATE as the join method.
	/// </summary>
	public ushort nwkManagerId;

	/// <summary>
	/// NWK Update ID. The value of the ZigBee nwkUpdateId known by the stack. This is used to determine the newest instance of the network after a PAN ID or channel change. This may only be set at joining when using SL_ZIGBEE_USE_CONFIGURED_NWK_STATE as the join method.
	/// </summary>
	public byte nwkUpdateId;

	/// <summary>
	/// NWK channel mask. The list of preferred channels that the NWK manager has told this device to use when searching for the network. This may only be set at joining when using SL_ZIGBEE_USE_CONFIGURED_NWK_STATE as the join method.
	/// </summary>
	public uint channels;

}

