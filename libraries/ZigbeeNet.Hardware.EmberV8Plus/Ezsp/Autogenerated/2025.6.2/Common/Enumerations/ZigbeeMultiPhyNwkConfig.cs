#if VERSION_2025_6_2
namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Network configuration for the desired radio interface for multi phy network.
/// </summary>
public enum ZigbeeMultiPhyNwkConfig : byte
{
	/// <summary>
	/// Enable broadcast support on Routers
	/// </summary>
    SL_ZIGBEE_BROADCAST_SUPPORT = 0x01
}

#endif