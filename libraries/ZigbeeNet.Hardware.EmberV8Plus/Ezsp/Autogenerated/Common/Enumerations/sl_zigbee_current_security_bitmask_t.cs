namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// System.Func`1[System.String]
/// </summary>
public enum ZigbeeCurrentSecurityBitmask : ushort
{
    /// <summary>
    /// This denotes that the device is running in a network with ZigBee Standard Security.
    /// </summary>
    SL_ZIGBEE_STANDARD_SECURITY_MODE = 0x0000,
    /// <summary>
    /// This denotes that the device is running in a network without a centralized Trust Center.
    /// </summary>
    SL_ZIGBEE_DISTRIBUTED_TRUST_CENTER_MODE = 0x0002,
    /// <summary>
    /// This denotes that the device has a Global Link Key. The Trust Center Link Key is the same across multiple nodes.
    /// </summary>
    SL_ZIGBEE_TRUST_CENTER_GLOBAL_LINK_KEY = 0x0004,
    /// <summary>
    /// This denotes that the node has a Trust Center Link Key.
    /// </summary>
    SL_ZIGBEE_HAVE_TRUST_CENTER_LINK_KEY = 0x0010,
    /// <summary>
    /// This denotes that the Trust Center is using a Hashed Link Key.
    /// </summary>
    SL_ZIGBEE_TRUST_CENTER_USES_HASHED_LINK_KEY = 0x0084
}
