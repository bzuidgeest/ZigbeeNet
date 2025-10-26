using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// Import an application link key into the key table.
/// Frame value: 0x010E
/// </summary>
public class secManImportLinkKeyResponse : EzspFrameResponse
{
    /// <summary>
    /// Status of key import operation.
    /// </summary>
    public sl_status_t status { get; set; }

}
