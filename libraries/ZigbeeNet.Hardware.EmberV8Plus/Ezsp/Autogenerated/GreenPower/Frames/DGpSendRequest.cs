using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.GreenPower.Frames;

/// <summary>
/// Adds/removes an entry from the GP Tx Queue.
/// Frame value: 0x00C6
/// </summary>
public class DGpSendRequest : EzspFrameRequest
{
    /// <summary>
    /// The action to perform on the GP TX queue (true to add, false to remove).
    /// </summary>
    public bool action { get; set; }

    /// <summary>
    /// Whether to use ClearChannelAssessment when transmitting the GPDF.
    /// </summary>
    public bool useCca { get; set; }

    /// <summary>
    /// The Address of the destination GPD.
    /// </summary>
    public sl_zigbee_gp_address_t addr { get; set; }

    /// <summary>
    /// The GPD command ID to send.
    /// </summary>
    public byte gpdCommandId { get; set; }

    /// <summary>
    /// The length of the GP command payload.
    /// </summary>
    public byte gpdAsduLength { get; set; }

    /// <summary>
    /// The GP command payload.
    /// </summary>
    public uint8_t[gpdAsduLength] gpdAsdu { get; set; }

    /// <summary>
    /// The handle to refer to the GPDF.
    /// </summary>
    public byte gpepHandle { get; set; }

    /// <summary>
    /// How long to keep the GPDF in the TX Queue.
    /// </summary>
    public ushort gpTxQueueEntryLifetimeMs { get; set; }

}
