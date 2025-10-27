using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// This call will update ZLL security token information. Unlike sli_zigbee_stack_zll_set_initial_security_state, this can be called while a network is already established.
/// Frame value: 0x00CF
/// </summary>
public class ZllSetSecurityStateWithoutKeyRequest : EzspFrameRequest
{
    /// <summary>
    /// Security state of the network.
    /// </summary>
    public sl_zigbee_zll_initial_security_state_t securityState { get; set; }

}
