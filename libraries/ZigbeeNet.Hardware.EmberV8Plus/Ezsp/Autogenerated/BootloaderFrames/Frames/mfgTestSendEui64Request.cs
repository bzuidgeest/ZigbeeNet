using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BootloaderFrames.Command;

/// <summary>
/// A function used during manufacturing configuration on the Golden Node to set the DUT&apos;s 8-byte EUI ID. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the new EUI ID within 150 milliseconds.
/// Frame value: 0x014A
/// </summary>
public class mfgTestSendEui64 : EzspFrameRequest
{
    /// <summary>
    /// The 8-byte EUID for the DUT.
    /// </summary>
    public sl_802154_long_addr_t newId { get; set; }

}
