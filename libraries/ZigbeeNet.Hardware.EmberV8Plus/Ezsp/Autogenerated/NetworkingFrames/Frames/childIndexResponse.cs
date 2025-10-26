using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Convert a node ID to a child index
/// Frame value: 0x0107
/// </summary>
public class childIndexResponse : EzspFrameResponse
{
    /// <summary>
    /// The child index or 0xFF if the node ID doesn&apos;t belong to a child
    /// </summary>
    public byte childIndex { get; set; }

}
