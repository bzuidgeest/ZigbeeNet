using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Utility
{
    public readonly record struct cMapping (string cSharpTypeName, int cByteSize, string description, bool isArray = false, int arraySize = 0);
    internal static class MapCTypes
    {

        private static readonly Dictionary<string, cMapping> CTypeMappings = new()
        {
            { "uint8_t", new ("byte", 1, string.Empty, false, 0) },
            { "int8_t", new ("sbyte", 1, string.Empty, false, 0) },
            { "uint16_t", new ("ushort", 2, string.Empty, false, 0) },
            { "int16_t", new ("short", 2, string.Empty, false, 0) },
            { "uint32_t", new ("uint", 4, string.Empty, false, 0) },
            { "int32_t", new ("int", 4, string.Empty, false, 0) },
            { "uint64_t", new ("ulong", 8, string.Empty, false, 0) },
            { "int64_t", new ("long", 8, string.Empty, false, 0) },
            { "bool", new ("bool", 1, string.Empty, false, 0) }
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
                return new cMapping(cType, 0, string.Empty, false, 0);
            }
        }
    }
}
