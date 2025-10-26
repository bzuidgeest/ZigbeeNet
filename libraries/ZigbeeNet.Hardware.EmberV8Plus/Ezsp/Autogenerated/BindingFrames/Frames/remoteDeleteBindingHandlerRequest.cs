using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.BindingFrames.Command;

/// <summary>
/// The NCP used the external binding modification policy to decide how to handle a remote delete binding request. The Host cannot change the current decision, but it can change the policy for future decisions using the &lt;i&gt;setPolicy&lt;/i&gt; command.
/// Frame value: 0x0032
/// </summary>
public class remoteDeleteBindingHandler : EzspFrameRequest
{
}
