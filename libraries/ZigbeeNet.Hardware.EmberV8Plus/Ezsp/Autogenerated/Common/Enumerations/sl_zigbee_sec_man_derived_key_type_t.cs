namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// System.Func`1[System.String]
/// </summary>
public enum ZigbeeSecManDerivedKeyType : ushort
{
    /// <summary>
    /// No derivation (use core key type directly).
    /// </summary>
    SL_ZB_SEC_MAN_DERIVED_KEY_TYPE_NONE = 0,
    /// <summary>
    /// Hash core key with Key Transport Key hash.
    /// </summary>
    SL_ZB_SEC_MAN_DERIVED_KEY_TYPE_KEY_TRANSPORT_KEY = 1,
    /// <summary>
    /// Hash core key with Key Load Key hash.
    /// </summary>
    SL_ZB_SEC_MAN_DERIVED_KEY_TYPE_KEY_LOAD_KEY = 2,
    /// <summary>
    /// Perform Verify Key hash.
    /// </summary>
    SL_ZB_SEC_MAN_DERIVED_KEY_TYPE_VERIFY_KEY = 3,
    /// <summary>
    /// Perform a simple AES hash of the key for TC backup.
    /// </summary>
    SL_ZB_SEC_MAN_DERIVED_KEY_TYPE_TC_SWAP_OUT_KEY = 4,
    /// <summary>
    /// For a TC using hashed link keys, hashed the root key against the supplied EUI in context.
    /// </summary>
    SL_ZB_SEC_MAN_DERIVED_KEY_TYPE_TC_HASHED_LINK_KEY = 5
}
