using YamlDotNet.Serialization;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Models
{
    /// <summary>
    /// Represents a typedef definition
    /// </summary>
    public class Typedef
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [YamlMember(Alias = "description")]
        public string Description { get; set; }

        [YamlMember(Alias = "definition")]
        [YamlConverter(typeof(TypedefDefinitionConverter))]
        public TypedefDefinition Definition { get; set; }
    }
}

