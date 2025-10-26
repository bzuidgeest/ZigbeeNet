using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.NetworkingFrames.Structure;

/// <summary>
/// Calling this function will render all other stack functions except sli_zigbee_stack_stack_power_up() non-functional until the radio is powered back on.
/// Frame value: 0x0143
/// </summary>
public class stackPowerDownResponse : EzspFrameResponse
{
}
