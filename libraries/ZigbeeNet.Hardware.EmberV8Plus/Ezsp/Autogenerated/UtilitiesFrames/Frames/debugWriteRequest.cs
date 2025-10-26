using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.UtilitiesFrames.Command;

/// <summary>
/// Sends a debug message from the Host to the Network Analyzer utility via the NCP.
/// Frame value: 0x0012
/// </summary>
public class debugWrite : EzspFrameRequest
{
    /// <summary>
    /// true if the message should be interpreted as binary data, false if the message should be interpreted as ASCII text.
    /// </summary>
    public bool binaryMessage { get; set; }

    /// <summary>
    /// The length of the &lt;i&gt;messageContents&lt;/i&gt; parameter in bytes.
    /// </summary>
    public byte messageLength { get; set; }

    /// <summary>
    /// The binary message.
    /// </summary>
    public uint8_t[messageLength] messageContents { get; set; }

}
