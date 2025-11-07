using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Utility;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Models;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Parser
{
    internal static class SimpleTypeDefinitionProcessor
    {
        public static string ProcessSimpleTypeDefinition(ILogger<EZSPYAMLDefinitionParser> logger, Typedef typedef, string definitionPath, string sectionName)
        {
            SimpleTypedefDefinition simple = (SimpleTypedefDefinition)typedef.Definition;

            // Types that do not reference header files in their description are simple types that map to C primitive types
            if (Regex.IsMatch(typedef.Description, "\\b[A-Za-z_]\\w*\\.h\\b") == false)
            {
                if (simple.Type.IndexOf('[') == -1)
                {
                    // Simple types map into basic c primitive types which are the basic set in the mapper.
                    cMapping baseType = MapCTypes.MapBaseCType(simple.Type);
                    MapCTypes.AddTypeMapping(typedef.Name, new cMapping(baseType.cSharpTypeName, baseType.cByteSize, typedef.Description));
                }
                else
                {
                    int arraySize = Regex.IsMatch(simple.Type, @"\[(\d+)\]")
                        ? int.Parse(Regex.Match(simple.Type, @"\[(\d+)\]").Groups[1].Value)
                        : 0;

                    cMapping baseType = MapCTypes.MapBaseCType(simple.Type.Substring(0, simple.Type.IndexOf('[')));
                    MapCTypes.AddTypeMapping(typedef.Name, new cMapping(baseType.cSharpTypeName, baseType.cByteSize, typedef.Description, true, arraySize));
                }

                logger.LogInformation("\t\t- {name} (simple): {type}", typedef.Name, simple.Type);

                return string.Empty;
            }
            else
            {
                // typedefs with descriptions that reference header files are enumerations
                MatchCollection headerFiles = Regex.Matches(typedef.Description, "\\b[A-Za-z_][A-Za-z0-9_-]*\\.h\\b");

                foreach (Match headerFile in headerFiles)
                {
                    if (File.Exists(Path.Combine(definitionPath, headerFile.Value)) == true)
                    {
                        EnumDefinition enumDefinition = new EnumDefinition
                        {
                            Name = typedef.Name,
                            Type = simple.Type,
                            Description = typedef.Description
                            //Items = headerFiles.Select((match, index) => new EnumItem
                            //{
                            //    Name = match.Value.Replace(".h", string.Empty).Trim(),
                            //    //Value = (ulong)index,
                            //    Description = $"Value imported from header file {match.Value.Trim()}"
                            //}).ToList()
                        };

                        string headerFileContent = File.ReadAllText(Path.Combine(definitionPath, headerFile.Value));
                        // Check if the header file contains an enum definition with the correct name
                        if (Regex.IsMatch(headerFileContent, $@"typedef\s+\w+\s+{typedef.Name}\s*;\s*enum", RegexOptions.Multiline))
                        {
                            // 1) Locate the block header for the target enum name:
                            //    Either: #ifdef DOXYGEN... enum <name>
                            //    Or:     typedef <ctype> <name>; enum
                            var headerRegex = new Regex(
                                $@"\#ifdef\s+DOXYGEN_SHOULD_SKIP_THIS(?:\r\n|\r|\n)enum\s+{typedef.Name}(?:\r\n|\r|\n)\#else(?:\r\n|\r|\n)typedef\s+(?<type>[a-zA-Z0-9_]+)\s+{typedef.Name};(?:\r\n|\r|\n)enum(?:\r\n|\r|\n)\#endif",
                                RegexOptions.Singleline | RegexOptions.IgnoreCase);

                            if (headerRegex.IsMatch(headerFileContent) == true)
                            {
                                var headerMatch = headerRegex.Match(headerFileContent);

                                // Try to capture the C type if present (typedef branch). If missing, leave unknown.
                                string cType = headerMatch.Groups.Count > 1 && headerMatch.Groups[1].Success
                                    ? headerMatch.Groups[1].Value
                                    : "unknown";

                                // 2) Starting from the header match, find the next enum body { ... };
                                //    We slice the text from the end of the header until the end of the enum body.
                                int searchStart = headerMatch.Index + headerMatch.Length;
                                string enumBlock = headerFileContent.Substring(searchStart, headerFileContent.IndexOf('}', searchStart) - searchStart);

                                // 3) Extract values + optional Doxygen comments (/** ... */)
                                var values = Regex.Matches(
                                        enumBlock,
                                        @"/\*\*\s*(?<comment>.*?)\s*\*/\s*(?<name>\w+)\s*,?",
                                        RegexOptions.Singleline)
                                    .Cast<Match>()
                                    .Select(m => (Name: m.Groups["name"].Value.Trim(), Comment: m.Groups["comment"].Value.Trim()))
                                    .Where(v => !string.IsNullOrWhiteSpace(v.Name))
                                    .ToList();

                                // 4) Populate enum items'
                                enumDefinition.Items.AddRange(
                                    values.Select(v => new EnumItem
                                    {
                                        Name = v.Name,
                                        Value = string.Empty, // Value is not defined in C enum, so we leave it empty
                                        Description = v.Comment
                                    })
                                );
                            }

                        }
                        // Check for enum by #define pattern
                        else if (Regex.IsMatch(headerFileContent, @$"^#define\s+(\w+)\s+\(\({typedef.Name}\)(0x[0-9A-Fa-f]+)\)\s*///<\s*(.+)$", RegexOptions.Multiline))
                        {
                            enumDefinition.Items.AddRange(
                                File.ReadAllLines(Path.Combine(definitionPath, headerFile.Value)).SelectMany(line =>
                                {
                                    var match = Regex.Match(
                                        line,
                                        @$"^#define\s+(\w+)\s+\(\({typedef.Name}\)(0x[0-9A-Fa-f]+)\)\s*///<\s*(.+)$"
                                    );

                                    if (match.Success)
                                    {
                                        var item = new EnumItem
                                        {
                                            Name = match.Groups[1].Value.Trim(),
                                            Value = match.Groups[2].Value.Trim(),
                                            Description = match.Groups[3].Value.Trim()
                                        };

                                        if (!string.IsNullOrWhiteSpace(item.Name) &&
                                            !string.IsNullOrWhiteSpace(item.Value) &&
                                            !string.IsNullOrWhiteSpace(item.Description))
                                        {
                                            return new[] { item };
                                        }
                                    }
                                    else
                                    {
                                        if (line.StartsWith("#define"))
                                        {
                                            logger.LogWarning("Failed to parse enum item in {headerFile} from line: {line}", headerFile, line);
                                        }
                                    }

                                    return Enumerable.Empty<EnumItem>();
                                })
                            );
                        }


                        if (enumDefinition.Items is not null && enumDefinition.Items.Count > 0)
                        {
                            return EnumDefinitionProcessor.ProcessEnumDefinition(logger, sectionName, enumDefinition);
                        }
                        else
                        {
                            logger.LogWarning("No enum items found in header file {headerFile} for enum {enumName}", headerFile.Value, typedef.Name);
                            return string.Empty;
                        }
                    }
                    else
                    {
                        logger.LogWarning("Referenced header file {headerFile} for enum {enumName} not found in path {definitionPath}", headerFile.Value, typedef.Name, definitionPath);
                        return string.Empty;
                    }
                }

                return string.Empty;
            }
        }
    }
}
