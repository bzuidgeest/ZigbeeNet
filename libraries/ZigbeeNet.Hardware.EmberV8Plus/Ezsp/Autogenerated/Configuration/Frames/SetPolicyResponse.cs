using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Allows the Host to change the policies used by the NCP to make fast decisions.
/// Frame value: 0x0055
/// </summary>
public class SetPolicyResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the policy was changed, SL_STATUS_ZIGBEE_EZSP_ERROR (for SL_ZIGBEE_EZSP_ERROR_INVALID_ID) if the NCP does not recognize &lt;i&gt;policyId&lt;/i&gt;.
    /// </summary>
    public sl_status_t status { get; set; }

}
