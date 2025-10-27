namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// Type of concentrator.
/// </summary>
public enum Emberconcentratortype : ushort
{
    /// <summary>
    /// A concentrator with insufficient memory to store source routes for the entire network. Route records are sent to the concentrator prior to every inbound APS unicast.
    /// </summary>
    SL_ZIGBEE_LOW_RAM_CONCENTRATOR = 0xFFF8,
    /// <summary>
    /// A concentrator with sufficient memory to store source routes for the entire network. Remote nodes stop sending route records once the concentrator has successfully received one.
    /// </summary>
    SL_ZIGBEE_HIGH_RAM_CONCENTRATOR = 0xFFF9
}
