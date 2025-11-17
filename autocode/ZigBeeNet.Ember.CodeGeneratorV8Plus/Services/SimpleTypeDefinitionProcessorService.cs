using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Models;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Models.TypeMapper;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Parser;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Utility;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Services
{
    internal class SimpleTypeDefinitionProcessorService
    {
        private readonly ILogger<SimpleTypeDefinitionProcessorService> _logger;
        private readonly TypeMapperService _typeMapperService;
        private readonly EnumDefinitionProcessorService _enumDefinitionProcessorService;
        private readonly FileService _FileService;

        public SimpleTypeDefinitionProcessorService(ILoggerFactory __loggerFactory, TypeMapperService typeMapperService, EnumDefinitionProcessorService enumDefinitionProcessorService, FileService fileService)
        {
            _logger = __loggerFactory.CreateLogger<SimpleTypeDefinitionProcessorService>();
            _typeMapperService = typeMapperService;
            _enumDefinitionProcessorService = enumDefinitionProcessorService;
            _FileService = fileService;
        }


        public bool Process(string sectionName, Typedef typeDefinition)
        {
            if (typeDefinition.IsSimple == false)
            {
                _logger.LogError("Type definition {typeName} is not a simple typedef", typeDefinition.Name);
                return false;
            }

            SimpleTypedefDefinition simpleTypedefDefinition = (SimpleTypedefDefinition)typeDefinition.Definition;
            TypeMapping? baseTypeMapping = null;
            int typeSizeInBytes = 0;
            string cSharpTypeName = Sanitize.TypeName(typeDefinition.Name);

            if (simpleTypedefDefinition.Type.IsArrayTypeDefinition() == true)
            {
                baseTypeMapping = _typeMapperService.GetTypeMapping(simpleTypedefDefinition.Type.GetArrayDefinitionBaseType());
                if (baseTypeMapping is not null)
                {
                    typeSizeInBytes = baseTypeMapping.Value.CType.SizeInBytes * simpleTypedefDefinition.Type.GetArrayDefinitionSize();
                    cSharpTypeName = baseTypeMapping.Value.CSharpType.Name;
                }
            }
            else if (typeDefinition.IsEnumInDisguise)
            {
                // This is a simple typedef that is actually an enum
                _logger.LogInformation("Processing simple typedef {typeName} as enum", typeDefinition.Name);
                baseTypeMapping = _typeMapperService.GetTypeMapping(simpleTypedefDefinition.Type);
                if (baseTypeMapping is not null)
                {
                    typeSizeInBytes = baseTypeMapping.Value.CType.SizeInBytes;
                }
                cSharpTypeName = Sanitize.EnumerationName(typeDefinition.Name);

                EnumDefinition enumDefinition = ParseSimpleTypeToEnumDefinition(typeDefinition);
                if (enumDefinition.Items is not null && enumDefinition.Items.Count > 0)
                {
                    _enumDefinitionProcessorService.Process(sectionName, enumDefinition);
                    return true;
                }
                else
                {
                    _logger.LogWarning("No enum items found for {0}", typeDefinition.Name);
                    return false;
                }
            }
            else
            {
                baseTypeMapping = _typeMapperService.GetTypeMapping(simpleTypedefDefinition.Type);
                if (baseTypeMapping is not null)
                {
                    typeSizeInBytes = baseTypeMapping.Value.CType.SizeInBytes;
                    cSharpTypeName = baseTypeMapping.Value.CSharpType.Name;
                }
            }

            if (baseTypeMapping is null)
            {
                _logger.LogError("No type mapping found for base type {type} of simple type {simpleType} in section {section}, cannot finish",
                    simpleTypedefDefinition.Type, typeDefinition.Name, sectionName);
                return false;
            }
            else
            {
                _typeMapperService.AddTypeMapping(
                    new CType(typeDefinition.Name, typeSizeInBytes, typeDefinition.Description)
                    {
                        IsArray = simpleTypedefDefinition.Type.IsArrayTypeDefinition(),
                        IsEnum = typeDefinition.IsEnumInDisguise,
                        SizeInBytes = typeSizeInBytes,
                        ArrayLength = simpleTypedefDefinition.Type.IsArrayTypeDefinition() ? simpleTypedefDefinition.Type.GetArrayDefinitionSize() : 0,
                    },
                    new CSharpType(cSharpTypeName)
                    {
                        IsArray = simpleTypedefDefinition.Type.IsArrayTypeDefinition(),
                        IsEnum = typeDefinition.IsEnumInDisguise,
                        ArrayLength = simpleTypedefDefinition.Type.IsArrayTypeDefinition() ? simpleTypedefDefinition.Type.GetArrayDefinitionSize() : 0,
                    }
                );
                _logger.LogInformation("\t\t- {name} (simple): {type}", typeDefinition.Name, simpleTypedefDefinition.Type);
                return true;
            }
        }

        private EnumDefinition ParseSimpleTypeToEnumDefinition(Typedef typedef)
        {
            SimpleTypedefDefinition simple = (SimpleTypedefDefinition)typedef.Definition;

            // typedefs with descriptions that reference header files are enumerations
            MatchCollection headerFiles = Regex.Matches(typedef.Description, "\\b[A-Za-z_][A-Za-z0-9_-]*\\.h\\b");

            EnumDefinition enumDefinition = new EnumDefinition
            {
                Name = typedef.Name,
                Type = simple.Type,
                Description = typedef.Description
            };

            foreach (Match headerFile in headerFiles)
            {
                if (_FileService.FileExistsInVersion(headerFile.Value) == true)
                {
                    string headerFileContent = _FileService.GetFile(headerFile.Value);

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
                        // if we found the enum definition, no need to check other header files
                        break;
                    }
                    // Check for enum by #define pattern
                    else if (Regex.IsMatch(headerFileContent, @$"^#define\s+(\w+)\s+\(\({typedef.Name}\)(0x[0-9A-Fa-f]+)\)\s*///<\s*(.+)$", RegexOptions.Multiline))
                    {
                        enumDefinition.Items.AddRange(
                            headerFileContent.ReadAllLines().SelectMany(line =>
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
                                        _logger.LogWarning("Failed to parse enum item in {headerFile} from line: {line}", headerFile, line);
                                    }
                                }

                                return Enumerable.Empty<EnumItem>();
                            })
                        );
                        // if we found the enum definition, no need to check other header files
                        break;
                    }
                    else
                    {
                        _logger.LogWarning("No enum definition found in header file {headerFile} for enum {enumName}", headerFile.Value, typedef.Name);
                    }
                }
                else
                {
                    _logger.LogWarning("Referenced header file {headerFile} for enum {enumName} not found.", headerFile.Value, typedef.Name);
                }
            }

            return enumDefinition;
        }
    }
}
