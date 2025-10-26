using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Command;

/// <summary>
/// A callback to inform the application that the Network Key has been updated and the node has been switched over to use the new key. The actual key being used is not passed up, but the sequence number is.
/// Frame value: 0x006e
/// </summary>
public class switchNetworkKeyHandler : EzspFrameRequest
{
}
