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

                        using TextReader headerFileReader = new StreamReader(Path.Combine(definitionPath, headerFile.Value));
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

                        return EnumDefinitionProcessor.ProcessEnumDefinition(logger, sectionName, enumDefinition);
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
