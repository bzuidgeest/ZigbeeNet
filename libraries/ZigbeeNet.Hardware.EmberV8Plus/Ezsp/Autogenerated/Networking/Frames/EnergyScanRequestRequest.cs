using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Sends a ZDO energy scan request. This request may only be sent by the current network manager and must be unicast, not broadcast. See ezsp-utils.h for related macros sli_zigbee_stack_set_network_manager_request() and sl_zigbee_change_channel_request().
/// Frame value: 0x009C
/// </summary>
public class EnergyScanRequestRequest : EzspFrameRequest
{
    /// <summary>
    /// The network address of the node to perform the scan.
    /// </summary>
    public sl_802154_short_addr_t target { get; set; }

    /// <summary>
    /// A mask of the channels to be scanned
    /// </summary>
    public uint scanChannels { get; set; }

    /// <summary>
    /// How long to scan on each channel. Allowed values are 0..5, with the scan times as specified by 802.15.4 (0 = 31ms, 1 = 46ms, 2 = 77ms, 3 = 138ms, 4 = 261ms, 5 = 507ms).
    /// </summary>
    public byte scanDuration { get; set; }

    /// <summary>
    /// The number of scans to be performed on each channel (1..8).
    /// </summary>
    public ushort scanCount { get; set; }

