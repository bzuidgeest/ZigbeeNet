using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Returns the maximum number of no-ack retries that will be attempted
/// Frame value: 0x006A
/// </summary>
public class getMaxMacRetries : EzspFrameRequest
{
}
