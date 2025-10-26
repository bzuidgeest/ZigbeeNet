using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Structure;

/// <summary>
/// Retrieve one of the cluster IDs associated with the given endpoint.
/// Frame value: 0x0131
/// </summary>
public class getEndpointClusterResponse : EzspFrameResponse
{
    /// <summary>
    /// ID of the requested cluster.
    /// </summary>
    public ushort endpoint_cluster { get; set; }

}
