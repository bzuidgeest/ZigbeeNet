using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Reads a value from the NCP.
/// Frame value: 0x00AA
/// </summary>
public class GetValueResponse : EzspFrameResponse
{
    /// <summary>
    /// SL_STATUS_OK if the value was read successfully, SL_STATUS_ZIGBEE_EZSP_ERROR otherwise.  Errors could be SL_ZIGBEE_EZSP_ERROR_INVALID_ID if the NCP does not recognize &lt;i&gt;valueId&lt;/i&gt;, SL_ZIGBEE_EZSP_ERROR_INVALID_VALUE if the length of the returned &lt;i&gt;value&lt;/i&gt; exceeds the size of local storage allocated to receive it.
    /// </summary>
    public sl_status_t status { get; set; }

    /// <summary>
    /// Both a command and response parameter. On command, the maximum size in bytes of local storage allocated to receive the returned &lt;i&gt;value&lt;/i&gt;. On response, the actual length in bytes of the returned &lt;i&gt;value&lt;/i&gt;.
    /// </summary>
    public byte valueLength { get; set; }

    /// <summary>
    /// The value.
    /// </summary>
    public uint8_t[valueLength] value { get; set; }

}
