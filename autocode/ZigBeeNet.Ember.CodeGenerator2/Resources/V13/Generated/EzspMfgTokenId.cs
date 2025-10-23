// Auto-generated file. Do not edit manually.

namespace ZigBeeNet.Ember.Enums
{
    /// <summary>
    /// EzspMfgTokenId enumeration
    /// </summary>
    public enum EzspMfgTokenId
    {
        EZSP_MFG_CUSTOM_VERSION = 0x00,
        /// <summary>
        /// Custom version (2 bytes).
        /// </summary>
        EZSP_MFG_STRING = 0x01,
        /// <summary>
        /// Manufacturing string (16 bytes).
        /// </summary>
        EZSP_MFG_BOARD_NAME = 0x02,
        /// <summary>
        /// Board name (16 bytes).
        /// </summary>
        EZSP_MFG_MANUF_ID = 0x03,
        /// <summary>
        /// Manufacturing ID (2 bytes).
        /// </summary>
        EZSP_MFG_PHY_CONFIG = 0x04,
        /// <summary>
        /// Radio configuration (2 bytes).
        /// </summary>
        EZSP_MFG_BOOTLOAD_AES_KEY = 0x05,
        /// <summary>
        /// Bootload AES key (16 bytes).
        /// </summary>
        EZSP_MFG_ASH_CONFIG = 0x06,
        /// <summary>
        /// ASH configuration (40 bytes).
        /// </summary>
        EZSP_MFG_EZSP_STORAGE = 0x07,
        /// <summary>
        /// EZSP storage (8 bytes).
        /// </summary>
        EZSP_STACK_CAL_DATA = 0x08,
        /// <summary>
        /// updated by the stack each time a calibration is performed.
        /// </summary>
        EZSP_MFG_CBKE_DATA = 0x09,
        /// <summary>
        /// Certificate Based Key Exchange (CBKE) data (92 bytes).
        /// </summary>
        EZSP_MFG_INSTALLATION_CODE = 0x0A,
        /// <summary>
        /// Installation code (20 bytes).
        /// </summary>
        EZSP_STACK_CAL_FILTER = 0x0B,
        /// <summary>
        /// calibration is performed.
        /// </summary>
        EZSP_MFG_CUSTOM_EUI_64 = 0x0C,
        /// <summary>
        /// Custom EUI64 MAC address (8 bytes).
        /// </summary>
        EZSP_MFG_CTUNE = 0x0D
    }
}
