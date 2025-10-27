using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Set the network update ID to the desired value. Must be called before joining or forming the network.
/// Frame value: 0x011D
/// </summary>
public class SetNwkUpdateIdRequest : EzspFrameRequest
{
    /// <summary>
    /// Desired value of the network update ID.
    /// </summary>
    public byte nwkUpdateId { get; set; }

    /// <summary>
    /// Set to true in case change should also apply when on network.
    /// </summary>
    public bool set_when_on_network { get; set; }

}
