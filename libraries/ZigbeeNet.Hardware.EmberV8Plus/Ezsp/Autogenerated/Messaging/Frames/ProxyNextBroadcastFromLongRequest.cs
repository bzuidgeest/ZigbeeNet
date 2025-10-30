using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Messaging.Frames;

/// <summary>
/// Sends proxied broadcast message for another node in conjunction with sl_zigbee_proxy_broadcast where a long source is also specified in the NWK frame control.
/// Frame value: 0x0066
/// </summary>
public class ProxyNextBroadcastFromLongRequest : EzspFrameRequest
{
    /// <summary>
    /// The long source from which to send the broadcast
    /// </summary>
    public uint8_t[8] euiSource { get; set; }

