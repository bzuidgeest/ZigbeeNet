using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Command;

/// <summary>
/// Set the ZLL data token.
/// Frame value: 0x00BD
/// </summary>
public class zllSetDataToken : EzspFrameRequest
{
    /// <summary>
    /// Data token to be set.
    /// </summary>
    public sl_zigbee_tok_type_stack_zll_data_t data { get; set; }

}
