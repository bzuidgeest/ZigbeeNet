using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBeeNet.Ember.CodeGeneratorV8Plus.Utility
{
    internal static class MapCTypes
    {
        /// <summary>
        /// Maps base C types to C# types.
        /// </summary>
        public static string MapBaseCType(string cType)
        {
            return cType.Trim() switch
            {
                "uint8_t" => "byte",
                "int8_t" => "sbyte",
                "uint16_t" => "ushort",
                "int16_t" => "short",
                "uint32_t" => "uint",
                "int32_t" => "int",
                "uint64_t" => "ulong",
                "int64_t" => "long",
                "bool" => "bool",
                _ => cType // Return original if no mapping found
            };
        }
    }
}
