using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BootloaderFrames.Structure;

/// <summary>
/// A function used during manufacturing configuration on the Golden Node to set the DUT&apos;s 16-byte configuration string. This function executes only during manufacturing configuration mode and will return an error otherwise. If successful, the DUT will acknowledge the new string within 150 milliseconds.
/// Frame value: 0x014B
/// </summary>
public class mfgTestSendManufacturingStringResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

}
