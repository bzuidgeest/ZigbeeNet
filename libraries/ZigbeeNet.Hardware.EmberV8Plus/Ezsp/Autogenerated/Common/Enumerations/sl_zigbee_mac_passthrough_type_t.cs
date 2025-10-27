namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;

/// <summary>
/// MAC passthrough message type flags.
/// </summary>
public enum ZigbeeMacPassthroughType : byte
{
    /// <summary>
    /// No MAC passthrough messages.
    /// </summary>
    SL_802154_PASSTHROUGH_NONE = 0x00,
    /// <summary>
    /// SE InterPAN messages.
    /// </summary>
    SL_802154_PASSTHROUGH_SE_INTERPAN = 0x01,
    /// <summary>
    /// Legacy EmberNet messages.
    /// </summary>
    SL_802154_PASSTHROUGH_EMBERNET = 0x02,
    /// <summary>
    /// Legacy EmberNet messages filtered by their source address.
    /// </summary>
    SL_802154_PASSTHROUGH_EMBERNET_SOURCE = 0x04
}
