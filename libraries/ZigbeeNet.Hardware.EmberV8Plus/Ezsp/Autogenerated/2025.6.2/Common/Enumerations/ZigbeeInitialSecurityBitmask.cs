#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// This is the Initial Security Bitmask that controls the use of various security features.
/// </summary>
public enum ZigbeeInitialSecurityBitmask : ushort
{
	/// <summary>
	/// This enables ZigBee Standard Security on the node.
	/// </summary>
	SL_ZIGBEE_STANDARD_SECURITY_MODE = 0x0000,
	/// <summary>
	/// This enables Distributed Trust Center Mode for the device forming the network. (Previously known as SL_ZIGBEE_NO_TRUST_CENTER_MODE)
	/// </summary>
	SL_ZIGBEE_DISTRIBUTED_TRUST_CENTER_MODE = 0x0002,
	/// <summary>
	/// This enables a Global Link Key for the Trust Center. All nodes will share the same Trust Center Link Key.
	/// </summary>
	SL_ZIGBEE_TRUST_CENTER_GLOBAL_LINK_KEY = 0x0004,
	/// <summary>
	/// This enables devices that perform MAC Association with a pre-configured Network Key to join the network. It is only set on the Trust Center.
	/// </summary>
	SL_ZIGBEE_PRECONFIGURED_NETWORK_KEY_MODE = 0x0008,
	/// <summary>
	/// This denotes that the preconfiguredKey is not the actual Link Key but a Secret Key known only to the Trust Center. It is hashed with the IEEE Address of the destination device in order to create the actual Link Key used in encryption. This is bit is only used by the Trust Center. The joining device need not set this.
	/// </summary>
	SL_ZIGBEE_TRUST_CENTER_USES_HASHED_LINK_KEY = 0x0084,
	/// <summary>
	/// This denotes that the preconfiguredKey element has valid data that should be used to configure the initial security state.
	/// </summary>
	SL_ZIGBEE_HAVE_PRECONFIGURED_KEY = 0x0100,
	/// <summary>
	/// This denotes that the networkKey element has valid data that should be used to configure the initial security state.
	/// </summary>
	SL_ZIGBEE_HAVE_NETWORK_KEY = 0x0200,
	/// <summary>
	/// This denotes to a joining node that it should attempt to acquire a Trust Center Link Key during joining. This is only necessary if the device does not have a pre-configured key.
	/// </summary>
	SL_ZIGBEE_GET_LINK_KEY_WHEN_JOINING = 0x0400,
	/// <summary>
	/// This denotes that a joining device should only accept an encrypted network key from the Trust Center (using its pre-configured key). A key sent in-the-clear by the Trust Center will be rejected and the join will fail. This option is only valid when utilizing a pre-configured key.
	/// </summary>
	SL_ZIGBEE_REQUIRE_ENCRYPTED_KEY = 0x0800,
	/// <summary>
	/// This denotes whether the device should NOT reset its outgoing frame counters (both NWK and APS) when ::sli_zigbee_stack_set_initial_security_state() is called. Normally it is advised to reset the frame counter before joining a new network. However in cases where a device is joining to the same network a again (but not using ::emberRejoinNetwork()) it should keep the NWK and APS frame counters stored in its tokens.
	/// </summary>
	SL_ZIGBEE_NO_FRAME_COUNTER_RESET = 0x1000,
	/// <summary>
	/// This denotes that the device should obtain its preconfigured key from an installation code stored in the manufacturing token. The token contains a value that will be hashed to obtain the actual preconfigured key. If that token is not valid, then the call to sli_zigbee_stack_set_initial_security_state() will fail.
	/// </summary>
	SL_ZIGBEE_GET_PRECONFIGURED_KEY_FROM_INSTALL_CODE = 0x2000,
	/// <summary>
	/// This denotes that the ::sl_zigbee_initial_security_state_t::preconfiguredTrustCenterEui64 has a value in it containing the trust center EUI64. The device will only join a network and accept commands from a trust center with that EUI64. Normally this bit is NOT set, and the EUI64 of the trust center is learned during the join process. When commissioning a device to join onto an existing network, which is using a trust center, and without sending any messages, this bit must be set and the field ::sl_zigbee_initial_security_state_t::preconfiguredTrustCenterEui64 must be populated with the appropriate EUI64.
	/// </summary>
	SL_ZIGBEE_HAVE_TRUST_CENTER_EUI64 = 0x0040
}

#endif