using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Structure;

/// <summary>
/// Get the ZLL tokens.
/// Frame value: 0x00BC
/// </summary>
public class zllGetTokensResponse : EzspFrameResponse
{
    /// <summary>
    /// Data token return value.
    /// </summary>
    public sl_zigbee_tok_type_stack_zll_data_t data { get; set; }

    /// <summary>
    /// Security token return value.
    /// </summary>
    public sl_zigbee_tok_type_stack_zll_security_t security { get; set; }

}
