using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Structure;

/// <summary>
/// Get the number of configured endpoints.
/// Frame value: 0x012F
/// </summary>
public class getEndpointCountResponse : EzspFrameResponse
{
    /// <summary>
    /// Number of configured endpoints.
    /// </summary>
    public byte count { get; set; }

}
