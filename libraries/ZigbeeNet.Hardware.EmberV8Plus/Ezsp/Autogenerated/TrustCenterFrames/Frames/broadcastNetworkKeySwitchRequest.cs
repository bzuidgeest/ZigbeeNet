using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenterFrames.Command;

/// <summary>
/// This function broadcasts a switch key message to tell all nodes to change to the sequence number of the previously sent Alternate Encryption Key.
/// Frame value: 0x0074
/// </summary>
public class broadcastNetworkKeySwitch : EzspFrameRequest
{
}
