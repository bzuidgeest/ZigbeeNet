using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// A callback invoked to inform the application that a stack token has changed.
/// Frame value: 0x000D
/// </summary>
public class StackTokenChangedHandlerResponse : EzspFrameResponse
{
    /// <summary>
    /// The address of the stack token that has changed.
    /// </summary>
    public ushort tokenAddress { get; set; }

}
