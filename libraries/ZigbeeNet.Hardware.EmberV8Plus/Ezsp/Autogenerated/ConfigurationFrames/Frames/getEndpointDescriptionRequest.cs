using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Command;

/// <summary>
/// Retrieve the endpoint description for the given endpoint number.
/// Frame value: 0x0130
/// </summary>
public class getEndpointDescription : EzspFrameRequest
{
    /// <summary>
    /// Endpoint number to get the description of.
    /// </summary>
    public byte endpoint { get; set; }

}
