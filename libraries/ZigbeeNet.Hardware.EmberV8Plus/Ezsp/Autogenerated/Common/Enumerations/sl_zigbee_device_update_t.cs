namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// The status of the device update.
/// </summary>
public enum ZigbeeDeviceUpdate : byte
{
    SL_ZIGBEE_STANDARD_SECURITY_SECURED_REJOIN = 0x0,
    SL_ZIGBEE_STANDARD_SECURITY_UNSECURED_JOIN = 0x1,
    SL_ZIGBEE_DEVICE_LEFT = 0x2,
    SL_ZIGBEE_STANDARD_SECURITY_UNSECURED_REJOIN = 0x3
}
