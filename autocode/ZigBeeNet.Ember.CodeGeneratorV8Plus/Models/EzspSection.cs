using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Models
{
    /// <summary>
    /// Represents a section in the ezsp.yaml file
    /// </summary>
    public class EzspSection
    {
        [YamlMember(Alias = "section")]
        public string? Section { get; set; }

        [YamlMember(Alias = "ncpCpps")]
        public object? NcpCpps { get; set; }

        [YamlMember(Alias = "typedefs")]
        public List<Typedef> Typedefs { get; set; } = new();

        [YamlMember(Alias = "enums")]
        public List<EnumDefinition> Enums { get; set; } = new();

        [YamlMember(Alias = "frames")]
        public List<Frame>? Frames { get; set; }
    }
}

