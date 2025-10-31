using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBeeNet.Ember.CodeGeneratorV8Plus.Utility
{
    public readonly record struct cMapping (string cSharpTypeName, int cByteSize);
    internal static class MapCTypes
    {

        private static readonly Dictionary<string, cMapping> CTypeMappings = new()
        {
            { "uint8_t", new ("byte", 1) },
            { "int8_t", new ("sbyte", 1) },
            { "uint16_t", new ("ushort", 2) },
            { "int16_t", new ("short", 2) },
            { "uint32_t", new ("uint", 4) },
            { "int32_t", new ("int", 4) },
            { "uint64_t", new ("ulong", 8) },
            { "int64_t", new ("long", 8) },
            { "bool", new ("bool", 1) }
        };

        public static void AddTypeMapping(string cType, cMapping mapTo)
        {
            CTypeMappings[cType] = mapTo;
        }

        /// <summary>
        /// Maps base C types to C# types.
        /// </summary>
        public static cMapping MapBaseCType(string cType)
        {
            bool mappingGood = CTypeMappings.TryGetValue(cType.Trim(), out cMapping cMapping);
            if (mappingGood == true)
            {
                return cMapping;
            }
            else
            {
                return new cMapping(cType, 0);
            }
        }
    }
}
