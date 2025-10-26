using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Set the current scheduler priorities for radio operations
/// Frame value: 0x012B
/// </summary>
public class radioSetSchedulerPriorities : EzspFrameRequest
{
    /// <summary>
    /// The current priorities.
    /// </summary>
    public sl_802154_radio_priorities_t priorities { get; set; }

}
