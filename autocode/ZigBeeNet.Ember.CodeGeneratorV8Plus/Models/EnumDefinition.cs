using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ZigBeeNet.Ember.CodeGenerator2.Models
{
    /// <summary>
    /// Represents an enum definition
    /// </summary>
    public class EnumDefinition
    {
        [YamlMember(Alias = "name")]
        public string Name { get; set; }

        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        [YamlMember(Alias = "description")]
        public string Description { get; set; }

        [YamlMember(Alias = "items")]
        public List<EnumItem> Items { get; set; } = new();
    }
}

