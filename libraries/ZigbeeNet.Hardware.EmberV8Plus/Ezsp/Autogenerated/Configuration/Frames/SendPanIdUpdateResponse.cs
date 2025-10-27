using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Triggers a pan id update message.
/// Frame value: 0x0057
/// </summary>
public class SendPanIdUpdateResponse : EzspFrameResponse
{
    /// <summary>
    /// true if the request was successfully handed to the stack, false otherwise
    /// </summary>
    public bool status { get; set; }

}
