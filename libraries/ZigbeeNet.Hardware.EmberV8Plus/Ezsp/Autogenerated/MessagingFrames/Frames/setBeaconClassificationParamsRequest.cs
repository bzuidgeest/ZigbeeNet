using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Sets the priority masks and related variables for choosing the best beacon.
/// Frame value: 0x00EF
/// </summary>
public class setBeaconClassificationParams : EzspFrameRequest
{
}
