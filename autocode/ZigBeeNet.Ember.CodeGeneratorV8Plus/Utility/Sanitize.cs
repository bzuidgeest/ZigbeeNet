using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBeeNet.Ember.CodeGeneratorV8Plus.Utility
{
    internal static class Sanitize
    {
        public static string SanitizeSectionName(string sectionName)
        {
            // Remove spaces and special characters, convert to PascalCase
            return System.Text.RegularExpressions.Regex.Replace(sectionName, @"[^a-zA-Z0-9]", "");
        }
    }
}
