using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Binding.Frames;

/// <summary>
/// The NCP used the external binding modification policy to decide how to handle a remote delete binding request. The Host cannot change the current decision, but it can change the policy for future decisions using the &lt;i&gt;setPolicy&lt;/i&gt; command.
/// Frame value: 0x0032
/// </summary>
public class RemoteDeleteBindingHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The index of the binding whose deletion was requested.
    /// </summary>
    public byte index { get; set; }

    /// <summary>
    /// SL_STATUS_OK if the binding was removed from the table and any other status if not.
    /// </summary>
    public sl_status_t policyDecision { get; set; }

}
