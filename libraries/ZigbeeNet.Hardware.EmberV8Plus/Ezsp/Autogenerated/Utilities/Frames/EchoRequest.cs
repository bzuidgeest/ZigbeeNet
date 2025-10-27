using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Utilities.Frames;

/// <summary>
/// Variable length data from the Host is echoed back by the NCP. This command has no other effects and is designed for testing the link between the Host and NCP.
/// Frame value: 0x0081
/// </summary>
public class EchoRequest : EzspFrameRequest
{
    /// <summary>
    /// The length of the &lt;i&gt;data&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte dataLength { get; set; }

    /// <summary>
    /// The data to be echoed back.
    /// </summary>
    public uint8_t[dataLength] data { get; set; }

}
