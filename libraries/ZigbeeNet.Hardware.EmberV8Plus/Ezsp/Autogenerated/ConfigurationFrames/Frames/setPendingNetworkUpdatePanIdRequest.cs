using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Command;

/// <summary>
/// Set the PAN ID to be accepted by the device in a NLME Network Update command.  If this is set to a different value than its default 0xFFFF, NLME network update messages will be ignored if they do not match this PAN ID.
/// Frame value: 0x011E
/// </summary>
public class setPendingNetworkUpdatePanId : EzspFrameRequest
{
    /// <summary>
    /// PAN ID to be accepted in a network update.
    /// </summary>
    public ushort panId { get; set; }

}
