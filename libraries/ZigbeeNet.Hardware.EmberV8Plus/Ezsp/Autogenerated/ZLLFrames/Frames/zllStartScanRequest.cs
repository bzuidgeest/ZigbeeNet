using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Command;

/// <summary>
/// This call will initiate a ZLL network scan on all the specified channels.
/// Frame value: 0x00B4
/// </summary>
public class zllStartScan : EzspFrameRequest
{
    /// <summary>
    /// The range of channels to scan.
    /// </summary>
    public uint channelMask { get; set; }

    /// <summary>
    /// The radio output power used for the scan requests.
    /// </summary>
    public sbyte radioPowerForScan { get; set; }

    /// <summary>
    /// The node type of the local device.
    /// </summary>
    public sl_zigbee_node_type_t nodeType { get; set; }

}
