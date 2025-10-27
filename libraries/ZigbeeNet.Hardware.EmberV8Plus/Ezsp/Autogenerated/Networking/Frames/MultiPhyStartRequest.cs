using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// This causes to initialize the desired radio interface other than native and form a new network by becoming the coordinator with same panId as native radio network.
/// Frame value: 0x00F8
/// </summary>
public class MultiPhyStartRequest : EzspFrameRequest
{
    /// <summary>
    /// Index of phy interface. The native phy index would be always zero hence valid phy index starts from one.
    /// </summary>
    public byte phyIndex { get; set; }

    /// <summary>
    /// Desired radio channel page.
    /// </summary>
    public byte page { get; set; }

    /// <summary>
    /// Desired radio channel.
    /// </summary>
    public byte channel { get; set; }

    /// <summary>
    /// Desired radio output power, in dBm.
    /// </summary>
    public sbyte power { get; set; }

    /// <summary>
    /// Network configuration bitmask.
    /// </summary>
    public sl_zigbee_multi_phy_nwk_config_t bitmask { get; set; }

}
