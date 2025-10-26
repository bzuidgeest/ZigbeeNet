namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// System.Func`1[System.String]
/// </summary>
public enum ZigbeeEzspMfgTokenId : byte
{
    /// <summary>
    /// Custom version (2 bytes).
    /// </summary>
    SL_ZIGBEE_EZSP_MFG_CUSTOM_VERSION = 0x00,
    /// <summary>
    /// Manufacturing string (16 bytes).
    /// </summary>
    SL_ZIGBEE_EZSP_MFG_STRING = 0x01,
    /// <summary>
    /// Board name (16 bytes).
    /// </summary>
    SL_ZIGBEE_EZSP_MFG_BOARD_NAME = 0x02,
    /// <summary>
    /// Manufacturing ID (2 bytes).
    /// </summary>
    SL_ZIGBEE_EZSP_MFG_MANUF_ID = 0x03,
    /// <summary>
    /// Radio configuration (2 bytes).
    /// </summary>
    SL_ZIGBEE_EZSP_MFG_PHY_CONFIG = 0x04,
    /// <summary>
    /// Bootload AES key (16 bytes).
    /// </summary>
    SL_ZIGBEE_EZSP_MFG_BOOTLOAD_AES_KEY = 0x05,
    /// <summary>
    /// ASH configuration (40 bytes).
    /// </summary>
    SL_ZIGBEE_EZSP_MFG_ASH_CONFIG = 0x06,
    /// <summary>
    /// EZSP storage (8 bytes).
    /// </summary>
    SL_ZIGBEE_EZSP_MFG_SL_ZIGBEE_EZSP_STORAGE = 0x07,
    /// <summary>
    /// Radio calibration data (64 bytes). 4 bytes are stored for each of the 16 channels. This token is not stored in the Flash Information Area. It is updated by the stack each time a calibration is performed.
    /// </summary>
    SL_ZIGBEE_EZSP_STACK_CAL_DATA = 0x08,
    /// <summary>
    /// Certificate Based Key Exchange (CBKE) data (92 bytes).
    /// </summary>
    SL_ZIGBEE_EZSP_MFG_CBKE_DATA = 0x09,
    /// <summary>
    /// Installation code (20 bytes).
    /// </summary>
    SL_ZIGBEE_EZSP_MFG_INSTALLATION_CODE = 0x0A,
    /// <summary>
    /// Radio channel filter calibration data (1 byte). This token is not stored in the Flash Information Area. It is updated by the stack each time a calibration is performed.
    /// </summary>
    SL_ZIGBEE_EZSP_STACK_CAL_FILTER = 0x0B,
    /// <summary>
    /// Custom EUI64 MAC address (8 bytes).
    /// </summary>
    SL_ZIGBEE_EZSP_MFG_CUSTOM_EUI_64 = 0x0C,
    /// <summary>
    /// CTUNE value (2 byte).
    /// </summary>
    SL_ZIGBEE_EZSP_MFG_CTUNE = 0x0D
}
