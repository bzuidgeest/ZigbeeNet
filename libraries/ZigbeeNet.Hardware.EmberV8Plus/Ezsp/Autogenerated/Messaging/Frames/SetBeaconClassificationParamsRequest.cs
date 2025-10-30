using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Sets the priority masks and related variables for choosing the best beacon.
/// Frame value: 0x00EF
/// </summary>
public class SetBeaconClassificationParamsRequest : EzspFrameRequest
{
