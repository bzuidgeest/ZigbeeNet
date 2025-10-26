namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// System.Func`1[System.String]
/// </summary>
public enum ZigbeeJoinMethod : byte
{
    /// <summary>
    /// Normally devices use MAC Association to join a network, which respects the &quot;permit joining&quot; flag in the MAC Beacon. This value should be used by default.
    /// </summary>
    SL_ZIGBEE_USE_MAC_ASSOCIATION = 0x0,
    /// <summary>
    /// For those networks where the &quot;permit joining&quot; flag is never turned on, they will need to use a ZigBee NWK Rejoin.  This value causes the rejoin to be sent without NWK security and the Trust Center will be asked to send the NWK key to the device. The NWK key sent to the device can be encrypted with the device&apos;s corresponding Trust Center link key.  That is determined by the ::sl_zigbee_join_decision_t on the Trust Center returned by the ::sl_zigbee_internal_trust_center_join_handler().
    /// </summary>
    SL_ZIGBEE_USE_NWK_REJOIN = 0x1,
    /// <summary>
    /// For those networks where the &quot;permit joining&quot; flag is never turned on, they will need to use an NWK Rejoin.  If those devices have been preconfigured with the  NWK key (including sequence number) they can use a secured rejoin.  This is only necessary for end devices since they need a parent.  Routers can simply use the ::SL_ZIGBEE_USE_CONFIGURED_NWK_STATE join method below.
    /// </summary>
    SL_ZIGBEE_USE_NWK_REJOIN_HAVE_NWK_KEY = 0x2,
    /// <summary>
    /// For those networks where all network and security information is known ahead of time, a router device may be commissioned such that it does not need to send any messages to begin communicating on the network.
    /// </summary>
    SL_ZIGBEE_USE_CONFIGURED_NWK_STATE = 0x3
}
