using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Utility
{
    internal static class MapCConstants
    {

        private static readonly Dictionary<string, string> cConstants = new()
        {
            
        };

        public static void AddConstantMapping(string name, string value)
        {
            cConstants.Add(name, value);
        }

        /// <summary>
        /// Maps base C constants to values.
        /// </summary>
        public static string MapConstant(string name)
        {
            bool mappingGood = cConstants.TryGetValue(name.Trim(), out string? value);
            if (mappingGood == true)
            {
                return value;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
