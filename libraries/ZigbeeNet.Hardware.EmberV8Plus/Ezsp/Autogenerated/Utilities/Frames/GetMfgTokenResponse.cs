using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Retrieves a manufacturing token from the Flash Information Area of the NCP (except for SL_ZIGBEE_EZSP_STACK_CAL_DATA which is managed by the stack).
/// Frame value: 0x000B
/// </summary>
public class GetMfgTokenResponse : EzspFrameResponse
{
    /// <summary>
    /// The length of the &lt;i&gt;tokenData&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte tokenDataLength { get; set; }

    /// <summary>
    /// The manufacturing token data.
    /// </summary>
    public uint8_t[tokenDataLength] tokenData { get; set; }

}
