using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Security.Frames;

/// <summary>
/// Gets the current security state that is being used by a device that is joined in the network.
/// Frame value: 0x0069
/// </summary>
public class GetCurrentSecurityStateResponse : EzspFrameResponse
{
    /// <summary>
    /// The success or failure code of the operation.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The security configuration in use by the stack.
    /// </summary>
    public sl_zigbee_current_security_state_t state { get; set; }

}
