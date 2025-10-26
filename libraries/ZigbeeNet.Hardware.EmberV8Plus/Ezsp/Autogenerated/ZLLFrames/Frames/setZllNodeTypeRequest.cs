using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLLFrames.Command;

/// <summary>
/// This call sets the default node type for a factory new ZLL device.
/// Frame value: 0x00D5
/// </summary>
public class setZllNodeType : EzspFrameRequest
{
    /// <summary>
    /// The node type to be set.
    /// </summary>
    public sl_zigbee_node_type_t nodeType { get; set; }

}
