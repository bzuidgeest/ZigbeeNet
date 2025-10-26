using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// Import a transient link key.
/// Frame value: 0x0111
/// </summary>
public class secManImportTransientKeyResponse : EzspFrameResponse
{
    /// <summary>
    /// Status of key import operation.
    /// </summary>
    public sl_status_t status { get; set; }

}
