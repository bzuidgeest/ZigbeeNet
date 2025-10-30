using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Retrieve the endpoint description for the given endpoint number.
/// Frame value: 0x0130
/// </summary>
public class GetEndpointDescriptionRequest : EzspFrameRequest
{
    /// <summary>
    /// Endpoint number to get the description of.
    /// </summary>
    public byte endpoint { get; set; }

