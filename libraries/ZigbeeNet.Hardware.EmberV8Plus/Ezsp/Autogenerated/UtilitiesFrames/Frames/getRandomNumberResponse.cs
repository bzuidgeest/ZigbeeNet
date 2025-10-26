using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Returns a pseudorandom number.
/// Frame value: 0x0049
/// </summary>
public class getRandomNumberResponse : EzspFrameResponse
{
    /// <summary>
    /// Always returns SL_STATUS_OK.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// A pseudorandom number.
    /// </summary>
    public ushort value { get; set; }

}
