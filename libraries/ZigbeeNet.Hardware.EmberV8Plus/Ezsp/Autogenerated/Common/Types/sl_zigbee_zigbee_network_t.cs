namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// The parameters of a ZigBee network.
/// </summary>
public struct sl_zigbee_zigbee_network_t
{
    /// <summary>
    /// The 802.15.4 channel associated with the network.
    /// </summary>
    public byte channel;

    /// <summary>
    /// The network's PAN identifier.
    /// </summary>
    public ushort panId;

    /// <summary>
    /// The network's extended PAN identifier.
    /// </summary>
    public fixed byte extendedPanId[8];

    /// <summary>
    /// Whether the network is allowing MAC associations.
    /// </summary>
    public bool allowingJoin;

    /// <summary>
    /// The Stack Profile associated with the network.
    /// </summary>
    public byte stackProfile;

    /// <summary>
    /// The instance of the Network.
    /// </summary>
    public byte nwkUpdateId;

}

