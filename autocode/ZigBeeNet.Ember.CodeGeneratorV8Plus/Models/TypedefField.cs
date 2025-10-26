using YamlDotNet.Serialization;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Models
{
    /// <summary>
    /// Represents a field within a typedef definition
    /// </summary>
    public class TypedefField
    {
        [YamlMember(Alias = "type")]
        public string? Type { get; set; }

        [YamlMember(Alias = "name")]
        public string? Name { get; set; }

        [YamlMember(Alias = "description")]
        public string? Description { get; set; }
    }
}

