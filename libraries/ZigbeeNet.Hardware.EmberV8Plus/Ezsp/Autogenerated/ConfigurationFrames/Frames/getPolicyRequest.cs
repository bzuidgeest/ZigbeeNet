using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Command;

/// <summary>
/// Allows the Host to read the policies used by the NCP to make fast decisions.
/// Frame value: 0x0056
/// </summary>
public class getPolicy : EzspFrameRequest
{
    /// <summary>
    /// Identifies which policy to read.
    /// </summary>
    public sl_zigbee_ezsp_policy_id_t policyId { get; set; }

}
