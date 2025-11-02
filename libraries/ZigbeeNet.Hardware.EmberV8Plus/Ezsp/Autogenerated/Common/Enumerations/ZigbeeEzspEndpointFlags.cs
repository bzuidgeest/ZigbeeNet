namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Flags associated with the endpoint data configured on the NCP.
/// </summary>
public enum ZigbeeEzspEndpointFlags : ushort
{
	/// <summary>
	/// Indicates that the endpoint is disabled and NOT discoverable via ZDO.
	/// </summary>
    SL_ZIGBEE_EZSP_ENDPOINT_DISABLED = 0x00,
	/// <summary>
	/// Indicates that the endpoint is enabled and discoverable via ZDO.
	/// </summary>
    SL_ZIGBEE_EZSP_ENDPOINT_ENABLED = 0x01
}
