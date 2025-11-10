using System.Text.RegularExpressions;
using YamlDotNet.Serialization;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Models
{
    /// <summary>
    /// Represents a typedef definition
    /// </summary>
    public class Typedef
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; } = string.Empty;

        [YamlMember(Alias = "description")]
        public string Description { get; set; } = string.Empty;

        [YamlMember(Alias = "definition")]
        [YamlConverter(typeof(TypedefDefinitionConverter))]
        public TypedefDefinition Definition { get; set; } 

        public bool IsComplex => Definition is ComplexTypedefDefinition;
        public bool IsSimple => Definition is SimpleTypedefDefinition;

        public bool IsEnumInDisguise =>
            IsComplex == false &&
            Definition is SimpleTypedefDefinition simpleTypedef &&
            Regex.IsMatch(Description, "\\b[A-Za-z_]\\w*\\.h\\b") == true;
    }
}

