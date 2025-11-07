#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Describes the type of ZigBee security key.
/// </summary>
public enum ZigbeeKeyType : byte
{
	/// <summary>
	/// A shared key between the Trust Center and a device.
	/// </summary>
    SL_ZIGBEE_TRUST_CENTER_LINK_KEY = 0x01,
	/// <summary>
	/// The current active Network Key used by all devices in the network.
	/// </summary>
    SL_ZIGBEE_CURRENT_NETWORK_KEY = 0x03,
	/// <summary>
	/// The alternate Network Key that was previously in use, or the newer key that will be switched to.
	/// </summary>
    SL_ZIGBEE_NEXT_NETWORK_KEY = 0x04,
	/// <summary>
	/// An Application Link Key shared with another (non-Trust Center) device.
	/// </summary>
    SL_ZIGBEE_APPLICATION_LINK_KEY = 0x05
}

#endif