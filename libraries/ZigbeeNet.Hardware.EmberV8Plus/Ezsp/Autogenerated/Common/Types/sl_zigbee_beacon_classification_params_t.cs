namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// The parameters related to beacon prioritization.
/// </summary>
public struct sl_zigbee_beacon_classification_params_t
{
    /// <summary>
    /// The min rssi value for receiving packets that is used is some beacon prioritization algorithms.
    /// </summary>
    public sbyte minRssiForReceivingPkts;

    /// <summary>
    /// The beacon classification mask that identifies which beacon prioritization algorithm to pick, and defines the relevant parameters.
    /// </summary>
    public ushort beaconClassificationMask;

}

