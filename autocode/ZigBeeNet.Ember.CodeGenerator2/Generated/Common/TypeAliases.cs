/// <summary>
/// 16-bit ZigBee network address.
/// </summary>
/// <remarks>Original C type: uint16_t</remarks>
global using sl_802154_short_addr_t = ushort;

/// <summary>
/// See sl_status.h for an enumerated list.
/// </summary>
/// <remarks>Original C type: uint32_t</remarks>
global using sl_status_t = uint;

/// <summary>
/// See enumeration in gp-types.h
/// </summary>
/// <remarks>Original C type: uint8_t</remarks>
global using sl_zigbee_gp_status_t = byte;

/// <summary>
/// 802.15.4 PAN ID.
/// </summary>
/// <remarks>Original C type: uint16_t</remarks>
global using sl_802154_pan_id_t = ushort;

/// <summary>
/// 16-bit ZigBee multicast group identifier.
/// </summary>
/// <remarks>Original C type: uint16_t</remarks>
global using sl_zigbee_multicast_id_t = ushort;

/// <summary>
/// EUI 64-bit ID (an IEEE address).
/// </summary>
/// <remarks>Original C type: uint8_t[8] (array types cannot be used in type aliases)</remarks>
// Skipped: sl_802154_long_addr_t

/// <summary>
/// The 8-bit identifier to uniquely identify the interface.
/// </summary>
/// <remarks>Original C type: uint8_t</remarks>
global using sl_zigbee_mac_interface_id_t = byte;

/// <summary>
/// A 16-byte array for the manufacturing string.
/// </summary>
/// <remarks>Original C type: uint8_t[16] (array types cannot be used in type aliases)</remarks>
// Skipped: sl_zigbee_manufacturing_string_t

/// <summary>
/// The percent of duty cycle for a limit. Duty Cycle, Limits, and Thresholds are reported in units of Percent * 100 (i.e. 10000 = 100.00%, 1 = 0.01%).
/// </summary>
/// <remarks>Original C type: uint16_t</remarks>
global using sl_zigbee_duty_cycle_hecto_pct_t = ushort;

/// <summary>
/// A library identifier
/// </summary>
/// <remarks>Original C type: uint8_t</remarks>
global using sl_zigbee_library_id_t = byte;

/// <summary>
/// This is a bitmask describing a filter for MAC data messages that the stack should accept and pass through to the application.
/// </summary>
/// <remarks>Original C type: uint16_t</remarks>
global using sl_zigbee_mac_filter_match_data_t = ushort;

