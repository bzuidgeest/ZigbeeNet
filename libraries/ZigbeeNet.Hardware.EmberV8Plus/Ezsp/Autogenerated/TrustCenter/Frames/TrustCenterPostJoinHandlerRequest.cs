using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.TrustCenter.Frames;

/// <summary>
/// The NCP uses the trust center behavior policy to decide whether to allow a new node to join the network (part of the trust center pre-join handler). The Host cannot change the current decision in this post-join callback, but it can change the policy for future decisions using the &lt;i&gt;setPolicy&lt;/i&gt; command.
/// Frame value: 0x0024
/// </summary>
public class TrustCenterPostJoinHandlerRequest : EzspFrameRequest
{
