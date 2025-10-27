using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Retrieve one of the cluster IDs associated with the given endpoint.
/// Frame value: 0x0131
/// </summary>
public class GetEndpointClusterResponse : EzspFrameResponse
{
    /// <summary>
    /// ID of the requested cluster.
    /// </summary>
    public ushort endpoint_cluster { get; set; }

}
