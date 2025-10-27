using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Extend a joiner&apos;s timeout to wait for the network key on the joiner default key timeout is 3 sec, and only values greater equal to 3 sec are accepted.
/// Frame value: 0x003A
/// </summary>
public class SetupDelayedJoinResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
