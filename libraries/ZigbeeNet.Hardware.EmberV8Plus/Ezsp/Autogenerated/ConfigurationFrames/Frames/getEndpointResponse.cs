using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Structure;

/// <summary>
/// Retrieve the endpoint number located at the specified index.
/// Frame value: 0x012E
/// </summary>
public class getEndpointResponse : EzspFrameResponse
{
    /// <summary>
    /// Endpoint number at the index.
    /// </summary>
    public byte endpoint { get; set; }

}
