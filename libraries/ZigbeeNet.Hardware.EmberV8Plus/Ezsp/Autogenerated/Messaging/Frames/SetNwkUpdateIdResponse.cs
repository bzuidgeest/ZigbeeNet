using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Set the network update ID to the desired value. Must be called before joining or forming the network.
/// Frame value: 0x011D
/// </summary>
public class SetNwkUpdateIdResponse : EzspFrameResponse
{
    /// <summary>
    /// Status of set operation for the network update ID.
    /// </summary>
    public sl_status_t status { get; set; }

}
