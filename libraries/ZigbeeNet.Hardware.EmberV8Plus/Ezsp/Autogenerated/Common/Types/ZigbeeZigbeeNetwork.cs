/// <summary>
/// The parameters of a ZigBee network.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeZigbeeNetwork
{
	/// <summary>
	/// The 802.15.4 channel associated with the network.
	/// </summary>
	public byte channel;

	/// <summary>
	/// The network&apos;s PAN identifier.
	/// </summary>
	public ushort panId;

	/// <summary>
	/// The network&apos;s extended PAN identifier.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] extendedPanId;
	/// <summary>
	/// Whether the network is allowing MAC associations.
	/// </summary>
	public bool allowingJoin;

	/// <summary>
	/// The Stack Profile associated with the network.
	/// </summary>
	public byte stackProfile;

	/// <summary>
	/// The instance of the Network.
	/// </summary>
	public byte nwkUpdateId;

}

