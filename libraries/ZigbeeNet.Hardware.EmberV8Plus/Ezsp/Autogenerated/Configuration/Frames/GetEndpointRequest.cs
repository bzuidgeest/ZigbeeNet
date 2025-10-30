using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Retrieve the endpoint number located at the specified index.
/// Frame value: 0x012E
/// </summary>
public class GetEndpointRequest : EzspFrameRequest
{
    /// <summary>
    /// Index to retrieve the endpoint number for.
    /// </summary>
    public byte index { get; set; }

