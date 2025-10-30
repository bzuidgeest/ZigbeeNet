using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ZLL.Frames;

/// <summary>
/// This call will cause the device to setup the security information used in its network. It must be called prior to forming, starting, or joining a network.
/// Frame value: 0x00B3
/// </summary>
public class ZllSetInitialSecurityStateRequest : EzspFrameRequest
{
    /// <summary>
    /// ZLL Network key.
    /// </summary>
    public sl_zigbee_key_data_t networkKey { get; set; }

    /// <summary>
    /// Initial security state of the network.
    /// </summary>
    public sl_zigbee_zll_initial_security_state_t securityState { get; set; }

