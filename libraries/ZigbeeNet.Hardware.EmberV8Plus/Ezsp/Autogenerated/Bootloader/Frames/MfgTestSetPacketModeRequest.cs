using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Bootloader.Frames;

/// <summary>
/// A function used on the Golden Node to switch between normal network operation (for testing) and manufacturing configuration. Like emberSleep(), it may not be possible to execute this command due to pending network activity. For the transition from normal network operation to manufacturing configuration, it is customary to loop, calling this function alternately with emberTick() until the mode change succeeds.
/// Frame value: 0x0148
/// </summary>
public class MfgTestSetPacketModeRequest : EzspFrameRequest
{
    /// <summary>
    /// Determines the new mode of operation. true causes the node to enter manufacturing configuration. false causes the node to return to normal network operation.
    /// </summary>
    public bool beginConfiguration { get; set; }

}
