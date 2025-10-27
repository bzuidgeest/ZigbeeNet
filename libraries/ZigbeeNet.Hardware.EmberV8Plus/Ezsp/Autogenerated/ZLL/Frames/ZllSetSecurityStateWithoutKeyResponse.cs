using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// This call will update ZLL security token information. Unlike sli_zigbee_stack_zll_set_initial_security_state, this can be called while a network is already established.
/// Frame value: 0x00CF
/// </summary>
public class ZllSetSecurityStateWithoutKeyResponse : EzspFrameResponse
{
    /// <summary>
    /// An sl_status_t value indicating success or the reason for failure.
    /// </summary>
    public sl_status_t status { get; set; }

}
