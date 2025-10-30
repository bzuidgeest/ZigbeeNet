using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Terminates a scan in progress.
/// Frame value: 0x001D
/// </summary>
public class StopScanRequest : EzspFrameRequest
{
