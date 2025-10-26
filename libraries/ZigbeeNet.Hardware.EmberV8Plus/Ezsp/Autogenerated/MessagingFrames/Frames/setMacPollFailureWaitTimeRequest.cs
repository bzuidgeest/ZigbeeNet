using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// This function is useful to sleepy end devices. This function will set the retry interval (in milliseconds) for mac data poll. This interval is the time in milliseconds the device waits before retrying a data poll when a MAC level data poll fails for any reason.
/// Frame value: 0x00F4
/// </summary>
public class setMacPollFailureWaitTime : EzspFrameRequest
{
    /// <summary>
    /// Time in milliseconds the device waits before retrying a data poll when a MAC level data poll fails for any reason.
    /// </summary>
    public uint waitBeforeRetryIntervalMs { get; set; }

}
