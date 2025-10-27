using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Sets the error code that is sent back from a router with a broken route. 
/// Frame value: 0x0011
/// </summary>
public class SetBrokenRouteErrorCodeRequest : EzspFrameRequest
{
    /// <summary>
    /// Desired error code.
    /// </summary>
    public byte errorCode { get; set; }

}
