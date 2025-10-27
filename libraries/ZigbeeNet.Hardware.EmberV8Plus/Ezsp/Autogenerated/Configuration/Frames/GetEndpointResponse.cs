using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Retrieve the endpoint number located at the specified index.
/// Frame value: 0x012E
/// </summary>
public class GetEndpointResponse : EzspFrameResponse
{
    /// <summary>
    /// Endpoint number at the index.
    /// </summary>
    public byte endpoint { get; set; }

}
