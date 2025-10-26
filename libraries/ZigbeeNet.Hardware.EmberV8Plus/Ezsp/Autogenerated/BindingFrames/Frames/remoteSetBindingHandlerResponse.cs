using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BindingFrames.Structure;

/// <summary>
/// The NCP used the external binding modification policy to decide how to handle a remote set binding request. The Host cannot change the current decision, but it can change the policy for future decisions using the &lt;i&gt;setPolicy&lt;/i&gt; command.
/// Frame value: 0x0031
/// </summary>
public class remoteSetBindingHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The requested binding.
    /// </summary>
    public sl_zigbee_binding_table_entry_t entry { get; set; }

    /// <summary>
    /// The index at which the binding was added.
    /// </summary>
    public byte index { get; set; }

    /// <summary>
    /// SL_STATUS_OK if the binding was added to the table and any other status if not.
    /// </summary>
    public sl_status_t policyDecision { get; set; }

}
