using System.Collections.Generic;

namespace ZigBeeNet.Ember.CodeGenerator2.Models
{
    /// <summary>
    /// Represents a typedef definition that can be either a simple string or a complex structure
    /// </summary>
    public abstract class TypedefDefinition
    {
    }

    /// <summary>
    /// Represents a simple string-based typedef definition (e.g., "uint16_t" or "uint8_t[8]")
    /// </summary>
    public class SimpleTypedefDefinition : TypedefDefinition
    {
        public string Type { get; set; }

        public SimpleTypedefDefinition(string type)
        {
            Type = type;
        }
    }

    /// <summary>
    /// Represents a complex typedef definition with multiple fields
    /// </summary>
    public class ComplexTypedefDefinition : TypedefDefinition
    {
        public List<TypedefField> Fields { get; set; } = new();

        public ComplexTypedefDefinition()
        {
        }

        public ComplexTypedefDefinition(List<TypedefField> fields)
        {
            Fields = fields;
        }
    }
}

