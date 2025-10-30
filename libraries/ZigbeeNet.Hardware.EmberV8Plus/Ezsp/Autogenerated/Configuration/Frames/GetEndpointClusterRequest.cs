using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Retrieve one of the cluster IDs associated with the given endpoint.
/// Frame value: 0x0131
/// </summary>
public class GetEndpointClusterRequest : EzspFrameRequest
{
    /// <summary>
    /// Endpoint number to get a cluster ID for.
    /// </summary>
    public byte endpoint { get; set; }

    /// <summary>
    /// Which list to get the cluster ID from.  (0 for input, 1 for output).
    /// </summary>
    public byte listId { get; set; }

    /// <summary>
    /// Index from requested list to look at the cluster ID of.
    /// </summary>
    public byte listIndex { get; set; }

