using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Update the GP Proxy table based on a GP pairing.
/// Frame value: 0x00C9
/// </summary>
public class GpProxyTableProcessGpPairingRequest : EzspFrameRequest
{
    /// <summary>
    /// The options field of the GP Pairing command.
    /// </summary>
    public uint options { get; set; }

    /// <summary>
    /// The target GPD.
    /// </summary>
    public sl_zigbee_gp_address_t addr { get; set; }

    /// <summary>
    /// The communication mode of the GP Sink.
    /// </summary>
    public byte commMode { get; set; }

    /// <summary>
    /// The network address of the GP Sink.
    /// </summary>
    public ushort sinkNetworkAddress { get; set; }

    /// <summary>
    /// The group ID of the GP Sink.
    /// </summary>
    public ushort sinkGroupId { get; set; }

    /// <summary>
    /// The alias assigned to the GPD.
    /// </summary>
    public ushort assignedAlias { get; set; }

    /// <summary>
    /// The IEEE address of the GP Sink.
    /// </summary>
    public uint8_t[8] sinkIeeeAddress { get; set; }

    /// <summary>
    /// The key to use for the target GPD.
    /// </summary>
    public sl_zigbee_key_data_t gpdKey { get; set; }

    /// <summary>
    /// The GPD security frame counter.
    /// </summary>
    public uint gpdSecurityFrameCounter { get; set; }

    /// <summary>
    /// The forwarding radius.
    /// </summary>
    public byte forwardingRadius { get; set; }

}
