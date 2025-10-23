using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ZigBeeNet.Ember.CodeGenerator2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Find all version folders in Resources directory
                // Look for Resources relative to the project directory
                string projectDir = Directory.GetCurrentDirectory();
                string resourcesPath = Path.Combine(projectDir, "Resources");

                // If not found, try going up directories to find the project root
                int attempts = 0;
                while (!Directory.Exists(resourcesPath) && attempts < 5)
                {
                    var parentDir = Directory.GetParent(projectDir);
                    if (parentDir == null) break;
                    projectDir = parentDir.FullName;
                    resourcesPath = Path.Combine(projectDir, "autocode", "ZigBeeNet.Ember.CodeGenerator2", "Resources");
                    attempts++;
                }

                if (!Directory.Exists(resourcesPath))
                {
                    Console.WriteLine($"Resources directory not found at: {resourcesPath}");
                    return;
                }

                var versionDirs = Directory.GetDirectories(resourcesPath)
                    .Where(d => Path.GetFileName(d).StartsWith("V") && int.TryParse(Path.GetFileName(d).Substring(1), out _))
                    .OrderBy(d => int.Parse(Path.GetFileName(d).Substring(1)))
                    .ToList();

                if (versionDirs.Count == 0)
                {
                    Console.WriteLine("No version folders (V13, V14, etc.) found in Resources directory");
                    return;
                }

                Console.WriteLine($"Found {versionDirs.Count} version folder(s)");

                foreach (var versionDir in versionDirs)
                {
                    string versionName = Path.GetFileName(versionDir);
                    string enumFilePath = Path.Combine(versionDir, "ezsp-enum.h");

                    if (!File.Exists(enumFilePath))
                    {
                        Console.WriteLine($"Warning: {enumFilePath} not found, skipping {versionName}");
                        continue;
                    }

                    Console.WriteLine($"\nProcessing {versionName}...");
                    ProcessVersion(versionDir, enumFilePath, versionName);
                }

                Console.WriteLine("\nCode generation completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        static void ProcessVersion(string versionDir, string enumFilePath, string versionName)
        {
            // Parse the enum file
            var enums = ParseEnumFile(enumFilePath);

            // Create output directory
            string outputDir = Path.Combine(versionDir, "Generated");
            Directory.CreateDirectory(outputDir);

            // Generate C# files for each enum
            foreach (var enumData in enums)
            {
                string csFileName = $"{enumData.TypeName}.cs";
                string csFilePath = Path.Combine(outputDir, csFileName);

                string csCode = GenerateCSharpEnum(enumData);
                File.WriteAllText(csFilePath, csCode);

                Console.WriteLine($"  Generated: {csFileName} ({enumData.Values.Count} values)");
            }

            // Parse protocol file if it exists
            string protocolFilePath = Path.Combine(versionDir, "ezsp-protocol.h");
            if (File.Exists(protocolFilePath))
            {
                var protocolConstants = ParseProtocolFile(protocolFilePath);

                if (protocolConstants.Count > 0)
                {
                    string csFileName = "EzspProtocol.cs";
                    string csFilePath = Path.Combine(outputDir, csFileName);
                    string csCode = GenerateCSharpProtocol(protocolConstants);
                    File.WriteAllText(csFilePath, csCode);
                    Console.WriteLine($"  Generated: {csFileName} ({protocolConstants.Count} constants)");
                }
            }
        }

        static List<EnumData> ParseEnumFile(string filePath)
        {
            var enums = new List<EnumData>();
            string content = File.ReadAllText(filePath);

            // Pattern to match typedef followed by enum
            // This pattern looks for: typedef TYPE NAME; ... enum { ... };
            // Allow flexible whitespace between typedef and enum
            string pattern = @"typedef\s+(\w+)\s+(\w+);\s*\n\s*\n\s*enum\s*\{";

            var matches = Regex.Matches(content, pattern, RegexOptions.Multiline);

            foreach (Match match in matches)
            {
                string baseType = match.Groups[1].Value;
                string typeName = match.Groups[2].Value;

                // Find the enum content starting from the opening brace
                int enumStart = match.Index + match.Length - 1; // Position of '{'
                int braceCount = 1;
                int pos = enumStart + 1;

                while (pos < content.Length && braceCount > 0)
                {
                    if (content[pos] == '{')
                        braceCount++;
                    else if (content[pos] == '}')
                        braceCount--;
                    pos++;
                }

                if (braceCount == 0)
                {
                    string enumContent = content.Substring(enumStart + 1, pos - enumStart - 2);
                    var values = ParseEnumValues(enumContent);

                    if (values.Count > 0)
                    {
                        var enumData = new EnumData
                        {
                            TypeName = typeName,
                            BaseType = baseType,
                            Values = values
                        };

                        enums.Add(enumData);
                    }
                }
            }

            return enums;
        }

        static List<EnumValue> ParseEnumValues(string enumContent)
        {
            var values = new List<EnumValue>();

            // Use regex to find all enum values
            // Pattern: optional comments, then NAME = VALUE
            string pattern = @"(?://[^\n]*\n\s*)*(\w+)\s*=\s*([^,\n}]+)";

            var matches = Regex.Matches(enumContent, pattern);

            foreach (Match match in matches)
            {
                string name = match.Groups[1].Value.Trim();
                string value = match.Groups[2].Value.Trim();

                // Extract preceding comments
                int matchStart = match.Index;
                string precedingText = enumContent.Substring(Math.Max(0, matchStart - 300), Math.Min(300, matchStart));

                var commentMatches = Regex.Matches(precedingText, @"//\s*([^\n]+)");
                string comment = "";
                if (commentMatches.Count > 0)
                {
                    // Get the last comment line
                    comment = commentMatches[commentMatches.Count - 1].Groups[1].Value.Trim();
                }

                values.Add(new EnumValue
                {
                    Name = name,
                    Value = value,
                    Comment = comment
                });
            }

            return values;
        }

        static string GenerateCSharpEnum(EnumData enumData)
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("// Auto-generated file. Do not edit manually.");
            sb.AppendLine();
            sb.AppendLine("namespace ZigBeeNet.Ember.Enums");
            sb.AppendLine("{");
            sb.AppendLine($"    /// <summary>");
            sb.AppendLine($"    /// {enumData.TypeName} enumeration");
            sb.AppendLine($"    /// </summary>");
            sb.AppendLine($"    public enum {enumData.TypeName}");
            sb.AppendLine("    {");

            for (int i = 0; i < enumData.Values.Count; i++)
            {
                var value = enumData.Values[i];

                if (!string.IsNullOrWhiteSpace(value.Comment))
                {
                    sb.AppendLine($"        /// <summary>");
                    sb.AppendLine($"        /// {value.Comment}");
                    sb.AppendLine($"        /// </summary>");
                }

                sb.Append($"        {value.Name} = {value.Value}");

                if (i < enumData.Values.Count - 1)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.AppendLine();
                }
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        static List<ProtocolConstant> ParseProtocolFile(string filePath)
        {
            var constants = new List<ProtocolConstant>();
            string content = File.ReadAllText(filePath);

            // Pattern to match #define statements
            // Matches: #define NAME VALUE
            // where VALUE can be a hex number, decimal, expression, or string
            string pattern = @"#define\s+(\w+)\s+(.+?)(?=\n|$)";

            var matches = Regex.Matches(content, pattern, RegexOptions.Multiline);

            foreach (Match match in matches)
            {
                string name = match.Groups[1].Value.Trim();
                string value = match.Groups[2].Value.Trim();

                // Remove inline comments from the value
                int commentIdx = value.IndexOf("//");
                if (commentIdx >= 0)
                {
                    value = value.Substring(0, commentIdx).Trim();
                }

                // Skip if value is empty or if it's a macro with parameters
                if (string.IsNullOrWhiteSpace(value) || value.Contains('('))
                    continue;

                // Skip preprocessor directives (like #ifdef, #ifndef, etc.)
                if (value.StartsWith('#'))
                    continue;

                // Skip if value contains backslash (line continuation)
                if (value.Contains('\\'))
                    continue;

                // Extract preceding comment
                int matchStart = match.Index;
                string precedingText = content.Substring(Math.Max(0, matchStart - 300), Math.Min(300, matchStart));

                var commentMatches = Regex.Matches(precedingText, @"//\s*([^\n]+)");
                string comment = "";
                if (commentMatches.Count > 0)
                {
                    comment = commentMatches[commentMatches.Count - 1].Groups[1].Value.Trim();
                }

                constants.Add(new ProtocolConstant
                {
                    Name = name,
                    Value = value,
                    Comment = comment
                });
            }

            return constants;
        }

        static string GenerateCSharpProtocol(List<ProtocolConstant> constants)
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("// Auto-generated file. Do not edit manually.");
            sb.AppendLine();
            sb.AppendLine("namespace ZigBeeNet.Ember.Protocol");
            sb.AppendLine("{");
            sb.AppendLine("    /// <summary>");
            sb.AppendLine("    /// EZSP Protocol constants and definitions");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine("    public static class EzspProtocol");
            sb.AppendLine("    {");

            foreach (var constant in constants)
            {
                if (!string.IsNullOrWhiteSpace(constant.Comment))
                {
                    sb.AppendLine($"        /// <summary>");
                    sb.AppendLine($"        /// {constant.Comment}");
                    sb.AppendLine($"        /// </summary>");
                }

                // Determine the type based on the value
                string type = DetermineConstantType(constant.Value);
                sb.AppendLine($"        public const {type} {constant.Name} = {constant.Value};");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            return sb.ToString();
        }

        static string DetermineConstantType(string value)
        {
            // Remove whitespace
            value = value.Trim();

            // Check for hex values
            if (value.StartsWith("0x") || value.StartsWith("0X"))
            {
                // Try to determine if it's a byte, ushort, or uint
                try
                {
                    long hexValue = Convert.ToInt64(value, 16);
                    if (hexValue <= byte.MaxValue)
                        return "byte";
                    else if (hexValue <= ushort.MaxValue)
                        return "ushort";
                    else
                        return "uint";
                }
                catch
                {
                    return "uint";
                }
            }

            // Check for decimal numbers
            if (int.TryParse(value, out int intVal))
            {
                if (intVal >= 0 && intVal <= byte.MaxValue)
                    return "byte";
                else if (intVal >= 0 && intVal <= ushort.MaxValue)
                    return "ushort";
                else
                    return "int";
            }

            // Check for expressions (contain operators)
            if (value.Contains("+") || value.Contains("-") || value.Contains("*") || value.Contains("/") || value.Contains("|") || value.Contains("<<"))
            {
                return "int";
            }

            // Default to int for unknown types
            return "int";
        }
    }

    class EnumData
    {
        public string TypeName { get; set; } = string.Empty;
        public string BaseType { get; set; } = string.Empty;
        public List<EnumValue> Values { get; set; } = new List<EnumValue>();
    }

    class EnumValue
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
    }

    class ProtocolConstant
    {
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
    }
}
