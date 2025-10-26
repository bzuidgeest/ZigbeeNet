namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Types;

/// <summary>
/// The calculated digest of a message
/// </summary>
public struct sl_zigbee_message_digest_t
{
    /// <summary>
    /// The calculated digest of a message.
    /// </summary>
    public fixed byte contents[16];

}

