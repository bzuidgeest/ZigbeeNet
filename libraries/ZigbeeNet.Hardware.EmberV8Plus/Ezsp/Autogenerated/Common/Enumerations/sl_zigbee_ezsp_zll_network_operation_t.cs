namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// System.Func`1[System.String]
/// </summary>
public enum ZigbeeEzspZllNetworkOperation : byte
{
    /// <summary>
    /// ZLL form network command.
    /// </summary>
    SL_ZIGBEE_EZSP_ZLL_FORM_NETWORK = 0x00,
    /// <summary>
    /// ZLL join target command.
    /// </summary>
    SL_ZIGBEE_EZSP_ZLL_JOIN_TARGET = 0x01
}
