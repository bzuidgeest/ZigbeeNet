namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// System.Func`1[System.String]
/// </summary>
public enum ZigbeeEventUnits : byte
{
    /// <summary>
    /// The event is not scheduled to run.
    /// </summary>
    SL_ZIGBEE_EVENT_INACTIVE = 0x00,
    /// <summary>
    /// The execution time is in approximate milliseconds.
    /// </summary>
    SL_ZIGBEE_EVENT_MS_TIME = 0x01,
    /// <summary>
    /// The execution time is in &apos;binary&apos; quarter seconds (256 approximate milliseconds each).
    /// </summary>
    SL_ZIGBEE_EVENT_QS_TIME = 0x02,
    /// <summary>
    /// The execution time is in &apos;binary&apos; minutes (65536 approximate milliseconds each).
    /// </summary>
    SL_ZIGBEE_EVENT_MINUTE_TIME = 0x03
}
