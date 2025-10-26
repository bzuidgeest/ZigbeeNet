using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Structure;

/// <summary>
/// Allows the Host to read the policies used by the NCP to make fast decisions.
/// Frame value: 0x0056
/// </summary>
public class getPolicyResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the policy was read successfully, SL_STATUS_ZIGBEE_EZSP_ERROR (for SL_ZIGBEE_EZSP_ERROR_INVALID_ID) if the NCP does not recognize &lt;i&gt;policyId&lt;/i&gt;.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// The current decision for the specified policy.
    /// </summary>
    public sl_zigbee_ezsp_decision_id_t decisionId { get; set; }

}
