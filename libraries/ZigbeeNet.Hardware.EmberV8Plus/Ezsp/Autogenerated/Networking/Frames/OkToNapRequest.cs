using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Networking.Frames;

/// <summary>
/// Indicate whether the stack is currently in a state where there are no high-priority tasks, allowing the device to sleep.
	/// There may be tasks expecting incoming messages, in which case the device should periodically wake up and call ::emberPollForData() in order to receive messages. This function can only be called when the node type is ::SL_ZIGBEE_SLEEPY_END_DEVICE
/// Frame value: 0x0146
/// </summary>
public class OkToNapRequest : EzspFrameRequest
{
