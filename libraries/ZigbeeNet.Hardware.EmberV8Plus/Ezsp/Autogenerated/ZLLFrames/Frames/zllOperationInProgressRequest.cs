using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Command;

/// <summary>
/// Is there a ZLL (Touchlink) operation in progress?
/// Frame value: 0x00D7
/// </summary>
public class zllOperationInProgress : EzspFrameRequest
{
}
