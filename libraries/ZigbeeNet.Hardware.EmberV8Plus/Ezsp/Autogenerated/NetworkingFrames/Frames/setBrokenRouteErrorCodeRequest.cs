using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Sets the error code that is sent back from a router with a broken route. 
/// Frame value: 0x0011
/// </summary>
public class setBrokenRouteErrorCode : EzspFrameRequest
{
    /// <summary>
    /// Desired error code.
    /// </summary>
    public byte errorCode { get; set; }

}
