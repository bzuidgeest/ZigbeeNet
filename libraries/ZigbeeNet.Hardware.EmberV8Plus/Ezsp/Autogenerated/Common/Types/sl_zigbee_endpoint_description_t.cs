namespace ZigBeeNet.EmberV8Plus.Common.Types;

/// <summary>
/// Description of a particular endpoint.
/// </summary>
public struct sl_zigbee_endpoint_description_t
{
    /// <summary>
    /// The endpoint's application profile.
    /// </summary>
    public ushort profileId;

    /// <summary>
    /// The endpoint's device ID within the application profile.
    /// </summary>
    public ushort deviceId;

    /// <summary>
    /// The endpoint's device version.
    /// </summary>
    public byte deviceVersion;

    /// <summary>
    /// The number of input clusters.
    /// </summary>
    public byte inputClusterCount;

    /// <summary>
    /// The number of output clusters.
    /// </summary>
    public byte outputClusterCount;

}

