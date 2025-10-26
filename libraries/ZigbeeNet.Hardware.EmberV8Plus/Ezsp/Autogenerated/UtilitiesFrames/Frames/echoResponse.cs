using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Structure;

/// <summary>
/// Variable length data from the Host is echoed back by the NCP. This command has no other effects and is designed for testing the link between the Host and NCP.
/// Frame value: 0x0081
/// </summary>
public class echoResponse : EzspFrameResponse
{
    /// <summary>
    /// The length of the &lt;i&gt;echo&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte echoLength { get; set; }

    /// <summary>
    /// The echo of the data.
    /// </summary>
    public uint8_t[echoLength] echo { get; set; }

}
