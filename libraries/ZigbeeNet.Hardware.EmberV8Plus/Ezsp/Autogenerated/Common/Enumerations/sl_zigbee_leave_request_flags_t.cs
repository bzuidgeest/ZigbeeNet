namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// System.Func`1[System.String]
/// </summary>
public enum ZigbeeLeaveRequestFlags : byte
{
    /// <summary>
    /// Leave and rejoin the network.
    /// </summary>
    SL_ZIGBEE_ZIGBEE_LEAVE_AND_REJOIN = 0x80,
    /// <summary>
    /// Leave the network and do not rejoin.
    /// </summary>
    SL_ZIGBEE_ZIGBEE_LEAVE_WITHOUT_REJOIN = 0x00
}
