using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Structure;

/// <summary>
/// Retrieve the endpoint description for the given endpoint number.
/// Frame value: 0x0130
/// </summary>
public class getEndpointDescriptionResponse : EzspFrameResponse
{
    /// <summary>
    /// Description of this endpoint.
    /// </summary>
    public sl_zigbee_endpoint_description_t result { get; set; }

}
