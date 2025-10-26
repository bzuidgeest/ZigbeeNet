using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BootloaderFrames.Structure;

/// <summary>
/// A function used during manufacturing configuration on the Golden Node to set the DUT&apos;s radio parameters. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the new parameters within 25 milliseconds.
/// Frame value: 0x014C
/// </summary>
public class mfgTestSendRadioParametersResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

}
