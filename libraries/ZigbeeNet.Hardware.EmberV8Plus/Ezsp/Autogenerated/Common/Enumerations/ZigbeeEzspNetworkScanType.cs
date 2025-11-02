namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Network scan types.
/// </summary>
public enum ZigbeeEzspNetworkScanType : byte
{
	/// <summary>
	/// An energy scan scans each channel for its RSSI value.
	/// </summary>
    SL_ZIGBEE_EZSP_ENERGY_SCAN = 0x00,
	/// <summary>
	/// An active scan scans each channel for available networks.
	/// </summary>
    SL_ZIGBEE_EZSP_ACTIVE_SCAN = 0x01
}
