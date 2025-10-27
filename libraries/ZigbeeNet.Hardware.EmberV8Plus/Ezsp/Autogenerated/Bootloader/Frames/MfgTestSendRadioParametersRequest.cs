using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// A function used during manufacturing configuration on the Golden Node to set the DUT&apos;s radio parameters. This function executes only during manufacturing configuration mode and returns an error otherwise. If successful, the DUT acknowledges the new parameters within 25 milliseconds.
/// Frame value: 0x014C
/// </summary>
public class MfgTestSendRadioParametersRequest : EzspFrameRequest
{
    /// <summary>
    /// Sets the radio band for the DUT. See ember-common.h for possible values.
    /// </summary>
    public byte supportedBands { get; set; }

    /// <summary>
    /// Sets the CC1020 crystal offset. This parameter has no effect on the EM2420, and it may safely be set to 0 for this RFIC.
    /// </summary>
    public sbyte crystalOffset { get; set; }

}
