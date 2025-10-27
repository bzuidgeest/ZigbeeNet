using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Binding.Frames;

/// <summary>
/// Deletes all binding table entries.
/// Frame value: 0x002A
/// </summary>
public class ClearBindingTableRequest : EzspFrameRequest
{
}
