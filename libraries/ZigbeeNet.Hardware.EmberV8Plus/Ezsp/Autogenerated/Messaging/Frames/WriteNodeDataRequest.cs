using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Write the current node Id, PAN ID, or Node type to the tokens
/// Frame value: 0x00FE
/// </summary>
public class WriteNodeDataRequest : EzspFrameRequest
{
    /// <summary>
    /// Erase the node type or not
    /// </summary>
    public bool erase { get; set; }

