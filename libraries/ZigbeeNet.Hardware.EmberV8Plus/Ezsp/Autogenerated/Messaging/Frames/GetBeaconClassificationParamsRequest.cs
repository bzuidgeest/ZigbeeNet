using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Gets the priority masks and related variables for choosing the best beacon.
/// Frame value: 0x00F3
/// </summary>
public class GetBeaconClassificationParamsRequest : EzspFrameRequest
{
