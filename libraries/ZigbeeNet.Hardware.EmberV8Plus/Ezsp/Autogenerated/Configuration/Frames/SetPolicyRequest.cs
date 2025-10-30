using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Allows the Host to change the policies used by the NCP to make fast decisions.
/// Frame value: 0x0055
/// </summary>
public class SetPolicyRequest : EzspFrameRequest
{
    /// <summary>
    /// Identifies which policy to modify.
    /// </summary>
    public sl_zigbee_ezsp_policy_id_t policyId { get; set; }

    /// <summary>
    /// The new decision for the specified policy.
    /// </summary>
    public sl_zigbee_ezsp_decision_id_t decisionId { get; set; }

