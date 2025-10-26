using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BootloaderFrames.Command;

/// <summary>
/// Transmits the given bootload message to a neighboring node using a specific 802.15.4 header that allows the EmberZNet stack as well as the bootloader to recognize the message, but will not interfere with other ZigBee stacks.
/// Frame value: 0x0090
/// </summary>
public class sendBootloadMessage : EzspFrameRequest
{
    /// <summary>
    /// If true, the destination address and pan id are both set to the broadcast address.
    /// </summary>
    public bool broadcast { get; set; }

    /// <summary>
    /// The EUI64 of the target node. Ignored if the broadcast field is set to true.
    /// </summary>
    public sl_802154_long_addr_t destEui64 { get; set; }

    /// <summary>
    /// The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte messageLength { get; set; }

    /// <summary>
    /// The multicast message.
    /// </summary>
    public uint8_t[messageLength] messageContents { get; set; }

}
