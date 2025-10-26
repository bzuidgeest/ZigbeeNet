using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.MessagingFrames.Command;

/// <summary>
/// Write the current node Id, PAN ID, or Node type to the tokens
/// Frame value: 0x00FE
/// </summary>
public class writeNodeData : EzspFrameRequest
{
    /// <summary>
    /// Erase the node type or not
    /// </summary>
    public bool erase { get; set; }

}
