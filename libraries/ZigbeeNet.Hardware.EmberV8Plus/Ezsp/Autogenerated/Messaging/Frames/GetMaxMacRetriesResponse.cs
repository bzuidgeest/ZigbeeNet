using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Returns the maximum number of no-ack retries that will be attempted
/// Frame value: 0x006A
/// </summary>
public class GetMaxMacRetriesResponse : EzspFrameResponse
{
    /// <summary>
    /// Max MAC retries
    /// </summary>
    public byte retries { get; set; }

}
