using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Allows the HOST to know whether the NCP is running the XNCP library. If so, the response contains also the manufacturer ID and the version number of the XNCP application that is running on the NCP.
/// Frame value: 0x0013
/// </summary>
public class getXncpInfoResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the NCP is running the XNCP library. SL_STATUS_INVALID_STATE otherwise.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The manufactured ID the user has defined in the XNCP application.
    /// </summary>
    public ushort manufacturerId { get; set; }

    /// <summary>
    /// The version number of the XNCP application.
    /// </summary>
    public ushort versionNumber { get; set; }

}
