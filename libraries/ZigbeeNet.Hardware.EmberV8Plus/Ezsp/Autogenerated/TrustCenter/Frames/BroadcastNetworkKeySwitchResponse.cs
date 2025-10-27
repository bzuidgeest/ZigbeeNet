using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenter.Frames;

/// <summary>
/// This function broadcasts a switch key message to tell all nodes to change to the sequence number of the previously sent Alternate Encryption Key.
/// Frame value: 0x0074
/// </summary>
public class BroadcastNetworkKeySwitchResponse : EzspFrameResponse
{
    /// <summary>
    /// sl_status_t value that indicates the success or failure of the command.
    /// </summary>
    public sl_status_t status { get; set; }

}
