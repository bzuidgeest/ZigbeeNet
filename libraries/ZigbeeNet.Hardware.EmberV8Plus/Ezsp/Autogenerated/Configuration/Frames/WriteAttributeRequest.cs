using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;

namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Configuration.Frames;

/// <summary>
/// Write attribute data on NCP endpoints.
/// Frame value: 0x0109
/// </summary>
public class WriteAttributeRequest : EzspFrameRequest
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

    /// <summary>
    /// Override read only and data type.
    /// </summary>
    public bool overrideReadOnlyAndDataType { get; set; }

    /// <summary>
    /// Override read only and data type.
    /// </summary>
    public bool justTest { get; set; }

    /// <summary>
    /// Attribute data type.
    /// </summary>
    public byte dataType { get; set; }

    /// <summary>
    /// Attribute data length.
    /// </summary>
    public byte dataLength { get; set; }

    /// <summary>
    /// Attribute data.
    /// </summary>
    public uint8_t[dataLength] data { get; set; }

