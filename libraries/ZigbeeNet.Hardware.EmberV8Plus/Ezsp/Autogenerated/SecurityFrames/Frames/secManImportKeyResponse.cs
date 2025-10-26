using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.SecurityFrames.Structure;

/// <summary>
/// Imports a key into security manager based on passed context.
/// Frame value: 0x0115
/// </summary>
public class secManImportKeyResponse : EzspFrameResponse
{
    /// <summary>
    /// The success or failure code of the operation.
    /// </summary>
    public sl_status_t status { get; set; }

}
