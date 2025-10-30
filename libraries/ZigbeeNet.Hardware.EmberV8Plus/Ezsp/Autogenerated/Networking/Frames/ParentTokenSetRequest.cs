using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Indicate whether the parent token has been set by association.
/// Frame value: 0x0140
/// </summary>
public class ParentTokenSetRequest : EzspFrameRequest
{
