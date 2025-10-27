using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Extend a joiner&apos;s timeout to wait for the network key on the joiner default key timeout is 3 sec, and only values greater equal to 3 sec are accepted.
/// Frame value: 0x003A
/// </summary>
public class SetupDelayedJoinRequest : EzspFrameRequest
{
    /// <summary>
    /// Network key timeout
    /// </summary>
    public byte networkKeyTimeoutS { get; set; }

}
