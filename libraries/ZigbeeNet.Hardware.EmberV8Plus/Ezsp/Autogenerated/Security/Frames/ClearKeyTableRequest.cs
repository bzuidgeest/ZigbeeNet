using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// This function clears the key table of the current network.
/// Frame value: 0x00B1
/// </summary>
public class ClearKeyTableRequest : EzspFrameRequest
{
