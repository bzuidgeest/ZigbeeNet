using ZigBeeNet.Hardware.Ember.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.ConfigurationFrames.Command;

/// <summary>
/// Read attribute data on NCP endpoints.
/// Frame value: 0x0108
/// </summary>
public class readAttribute : EzspFrameRequest
{
    /// <summary>
    /// Endpoint
    /// </summary>
    public byte endpoint { get; set; }

    /// <summary>
    /// Cluster.
    /// </summary>
    public ushort cluster { get; set; }

    /// <summary>
    /// Attribute ID.
    /// </summary>
    public ushort attributeId { get; set; }

    /// <summary>
    /// Mask.
    /// </summary>
    public byte mask { get; set; }

    /// <summary>
    /// Manufacturer code.
    /// </summary>
    public ushort manufacturerCode { get; set; }

}
