using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Return radio power value of the child from the given childIndex
/// Frame value: 0x0134
/// </summary>
public class ChildPowerResponse : EzspFrameResponse
{
    /// <summary>
    /// The power of the child or maximum radio power, which is the power value provided by the user while forming/joining a network if there isn&apos;t a child at the childIndex specified
    /// </summary>
    public sbyte childPower { get; set; }

}
