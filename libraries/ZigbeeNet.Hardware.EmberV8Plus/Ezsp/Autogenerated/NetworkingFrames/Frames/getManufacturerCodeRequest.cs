using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Command;

/// <summary>
/// Gets the manufacturer code to the specified value. The manufacturer code is one of the fields of the node descriptor.
/// Frame value: 0x00CA
/// </summary>
public class getManufacturerCode : EzspFrameRequest
{
}
