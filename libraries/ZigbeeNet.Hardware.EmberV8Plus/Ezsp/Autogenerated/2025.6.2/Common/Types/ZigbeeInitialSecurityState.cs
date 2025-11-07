#if VERSION_2025_6_2
/// <summary>
/// The security data used to set the configuration for the stack, or the retrieved configuration currently in use.
/// </summary>

using System.Runtime.InteropServices;
using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

[StructLayout(LayoutKind.Sequential)]
public struct ZigbeeInitialSecurityState
{
	/// <summary>
	/// A bitmask indicating the security state used to indicate what the security configuration will be when the device forms or joins the network.
	/// </summary>
	public ZigbeeInitialSecurityBitmask bitmask;

	/// <summary>
	/// The pre-configured Key data that should be used when forming or joining the network. The security bitmask must be set with the SL_ZIGBEE_HAVE_PRECONFIGURED_KEY bit to indicate that the key contains valid data.
	/// </summary>
	public ZigbeeKeyData preconfiguredKey;

	/// <summary>
	/// The Network Key that should be used by the Trust Center when it forms the network, or the Network Key currently in use by a joined device. The security bitmask must be set with SL_ZIGBEE_HAVE_NETWORK_KEY to indicate that the key contains valid data.
	/// </summary>
	public ZigbeeKeyData networkKey;

	/// <summary>
	/// The sequence number associated with the network key. This is only valid if the SL_ZIGBEE_HAVE_NETWORK_KEY has been set in the security bitmask.
	/// </summary>
	public byte networkKeySequenceNumber;

	/// <summary>
	/// This is the long address of the trust center on the network that will be joined. It is usually NOT set prior to joining the network and instead it is learned during the joining message exchange. This field is only examined if ::SL_ZIGBEE_HAVE_TRUST_CENTER_EUI64 is set in the sl_zigbee_initial_security_state_t::bitmask. Most devices should clear that bit and leave this field alone. This field must be set when using commissioning mode.
	/// </summary>
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
	public byte[] preconfiguredTrustCenterEui64;

}


#endif