using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Clear all of the transient link keys from RAM.
/// Frame value: 0x006B
/// </summary>
public class ClearTransientLinkKeysRequest : EzspFrameRequest
{
