namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// System.Func`1[System.String]
/// </summary>
public enum ZigbeeJoinDecision : byte
{
    /// <summary>
    /// Allow the node to join. The joining node should have a pre-configured key. The security data sent to it will be encrypted with that key.
    /// </summary>
    SL_ZIGBEE_USE_PRECONFIGURED_KEY = 0x00,
    /// <summary>
    /// Allow the node to join. Send the network key in-the-clear to the joining device.
    /// </summary>
    SL_ZIGBEE_SEND_KEY_IN_THE_CLEAR = 0x01,
    /// <summary>
    /// Deny join.
    /// </summary>
    SL_ZIGBEE_DENY_JOIN = 0x02,
    /// <summary>
    /// Take no action.
    /// </summary>
    SL_ZIGBEE_NO_ACTION = 0x03
}
