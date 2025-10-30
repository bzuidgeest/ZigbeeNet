using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// A callback invoked when the status of the stack changes. If the status parameter equals SL_STATUS_NETWORK_UP, then the &lt;i&gt;getNetworkParameters&lt;/i&gt; command can be called to obtain the new network parameters. If any of the parameters are being stored in nonvolatile memory by the Host, the stored values should be updated.
/// Frame value: 0x0019
/// </summary>
public class StackStatusHandlerRequest : EzspFrameRequest
{
