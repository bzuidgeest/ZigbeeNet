using YamlDotNet.Serialization;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Models
{
    /// <summary>
    /// Represents an item within an enum
    /// </summary>
    public class EnumItem
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [YamlMember(Alias = "value")]
        public string Value { get; set; }

        [YamlMember(Alias = "description")]
        public string Description { get; set; }
    }
}

