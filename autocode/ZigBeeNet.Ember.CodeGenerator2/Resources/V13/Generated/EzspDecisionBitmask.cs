// Auto-generated file. Do not edit manually.

namespace ZigBeeNet.Ember.Enums
{
    /// <summary>
    /// EzspDecisionBitmask enumeration
    /// </summary>
    public enum EzspDecisionBitmask
    {
        EZSP_DECISION_BITMASK_DEFAULT_CONFIGURATION = 0x0000,
        /// <summary>
        /// Disallow joins and rejoins.
        /// </summary>
        EZSP_DECISION_ALLOW_JOINS = 0x0001,
        /// <summary>
        /// Send the network key to all joining devices.
        /// </summary>
        EZSP_DECISION_ALLOW_UNSECURED_REJOINS = 0x0002,
        /// <summary>
        /// Send the network key to all rejoining devices.
        /// </summary>
        EZSP_DECISION_SEND_KEY_IN_CLEAR = 0x0004,
        /// <summary>
        /// Send the network key in the clear.
        /// </summary>
        EZSP_DECISION_IGNORE_UNSECURED_REJOINS = 0x0008,
        /// <summary>
        /// Do nothing for unsecured rejoins.
        /// </summary>
        EZSP_DECISION_JOINS_USE_INSTALL_CODE_KEY = 0x0010,
        /// <summary>
        /// Allow joins if there is an entry in the transient key table.
        /// </summary>
        EZSP_DECISION_DEFER_JOINS = 0x0020
    }
}
