using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Indicate whether the stack currently has any tasks pending. If no tasks are pending, ::emberTick() does not need to be called until the next time a stack API function is called. This function can only be called when the node type is ::SL_ZIGBEE_SLEEPY_END_DEVICE.
/// Frame value: 0x0141
/// </summary>
public class okToHibernateResponse : EzspFrameResponse
{
    /// <summary>
    /// True if the application may sleep for as long as it wishes.
    /// </summary>
    public bool indicator { get; set; }

}
