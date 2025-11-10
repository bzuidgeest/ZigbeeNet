using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Utility
{
    internal static class Sanitize
    {
        public static string SectionName(string sectionName)
        {
            // Remove spaces and special characters, convert to PascalCase
            return System.Text.RegularExpressions.Regex.Replace(sectionName, @"[^a-zA-Z0-9]", "").Replace("Frames", "");
        }

        public static string EnumerationName(string enumerationName)
        {
            // Remove spaces and special characters, convert to PascalCase
            if (string.IsNullOrWhiteSpace(enumerationName))
                return enumerationName;

            // Remove sl_ prefix if present
            if (enumerationName.StartsWith("sl_", StringComparison.OrdinalIgnoreCase))
                enumerationName = enumerationName[3..];

            // Remove _t suffix if present
            if (enumerationName.EndsWith("_t", StringComparison.OrdinalIgnoreCase))
                enumerationName = enumerationName[..^2];

            // Split by underscores and non-alphanumeric characters
            var parts = System.Text.RegularExpressions.Regex.Split(enumerationName, @"[_\W]+");

            // PascalCase: capitalize first letter of each part
            var pascalCased = string.Concat(parts.Select(part =>
                part.Length > 0 ? char.ToUpper(part[0]) + part[1..].ToLower() : ""));

            // Check if reserved keyword and prefix with @ if needed
            if (IsReservedKeyword(pascalCased))
            {
                pascalCased = "@" + pascalCased;
            }

            return pascalCased;
        }

        public static string StructureName(string enumerationName)
        {
            // Remove spaces and special characters, convert to PascalCase
            if (string.IsNullOrWhiteSpace(enumerationName))
                return enumerationName;

            // Remove sl_ prefix if present
            if (enumerationName.StartsWith("sl_", StringComparison.OrdinalIgnoreCase))
                enumerationName = enumerationName[3..];

            // Remove _t suffix if present
            if (enumerationName.EndsWith("_t", StringComparison.OrdinalIgnoreCase))
                enumerationName = enumerationName[..^2];

            // Split by underscores and non-alphanumeric characters
            var parts = System.Text.RegularExpressions.Regex.Split(enumerationName, @"[_\W]+");

            // PascalCase: capitalize first letter of each part
            var pascalCased = string.Concat(parts.Select(part =>
                part.Length > 0 ? char.ToUpper(part[0]) + part[1..].ToLower() : ""));

            // Check if reserved keyword and prefix with @ if needed
            if (IsReservedKeyword(pascalCased))
            {
                pascalCased = "@" + pascalCased;
            }

            return pascalCased;
        }

        public static string TypeName(string typeName)
        {
            // Remove spaces and special characters, convert to PascalCase
            if (string.IsNullOrWhiteSpace(typeName))
                return typeName;

            // Remove sl_ prefix if present
            if (typeName.StartsWith("sl_", StringComparison.OrdinalIgnoreCase))
                typeName = typeName[3..];

            // Remove _t suffix if present
            if (typeName.EndsWith("_t", StringComparison.OrdinalIgnoreCase))
                typeName = typeName[..^2];

            if (IsReservedKeyword(typeName) == true)
            {
                return typeName;
            }
            else
            {
                // Split by underscores and non-alphanumeric characters
                var parts = System.Text.RegularExpressions.Regex.Split(typeName, @"[_\W]+");

                // PascalCase: capitalize first letter of each part
                var pascalCased = string.Concat(parts.Select(part =>
                    part.Length > 0 ? char.ToUpper(part[0]) + part[1..].ToLower() : ""));

                // Check if reserved keyword and prefix with @ if needed
                if (IsReservedKeyword(typeName) == true)
                {
                    pascalCased = "@" + pascalCased;
                }
                return pascalCased;
            }
        }

        public static string PropertyName(string propertyName)
        {
            // Remove spaces and special characters, convert to PascalCase
            if (string.IsNullOrWhiteSpace(propertyName))
                return propertyName;

            // Remove sl_ prefix if present
            if (propertyName.StartsWith("sl_", StringComparison.OrdinalIgnoreCase))
                propertyName = propertyName[3..];

            // Remove _t suffix if present
            if (propertyName.EndsWith("_t", StringComparison.OrdinalIgnoreCase))
                propertyName = propertyName[..^2];

            // Split by underscores and non-alphanumeric characters
            var parts = System.Text.RegularExpressions.Regex.Split(propertyName, @"[_\W]+");

            // PascalCase: capitalize first letter of each part
            var pascalCased = string.Concat(parts.Select(part =>
                part.Length > 0 ? char.ToUpper(part[0]) + part[1..] : ""));

            // Check if reserved keyword and prefix with @ if needed
            if (IsReservedKeyword(pascalCased))
            {
                pascalCased = "@" + pascalCased;
            }

            return pascalCased;
        }

        public static bool IsReservedKeyword(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return false;

            // Convert to lowercase for case-insensitive comparison
            return SyntaxFacts.GetKeywordKind(word.ToLower()) != SyntaxKind.None;
        }

        /// <summary>
        /// Escapes special XML characters in a string for use in XML documentation.
        /// </summary>
        public static string XmlEscape(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }

        public static string AddXmlCommentPrefixAfterLineBreak(this string text, int tabCount = 0)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // Create the indentation string
            string indentation = new string('\t', tabCount);

            // Match any line ending (Windows \r\n or Unix \n)
            return System.Text.RegularExpressions.Regex.Replace(
                text,
                @"\r?\n",
                "\r\n" + indentation + "/// "
            );
        }
    }
}
