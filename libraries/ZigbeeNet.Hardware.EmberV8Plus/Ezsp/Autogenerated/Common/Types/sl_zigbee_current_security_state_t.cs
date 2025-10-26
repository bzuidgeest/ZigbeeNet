namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// The security options and information currently used by the stack.
/// </summary>
public struct sl_zigbee_current_security_state_t
{
    /// <summary>
    /// A bitmask indicating the security options currently in use by a device joined in the network.
    /// </summary>
    public sl_zigbee_current_security_bitmask_t bitmask;

    /// <summary>
    /// The IEEE Address of the Trust Center device.
    /// </summary>
    public sl_802154_long_addr_t trustCenterLongAddress;

}

