using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Utility;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Models;
using System.Globalization;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Services;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Models.TypeMapper;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Parser
{
    internal class EZSPYAMLDefinitionParser
    {
        private readonly ILogger<EZSPYAMLDefinitionParser> _logger;
        private readonly ApplicationSettings _settings;
        private string _versionName = string.Empty;
        private readonly FrameDefinitionProcessorService _frameDefinitionProcessorService;
        private readonly TypeMapperService _tyepMapperService;

        public EZSPYAMLDefinitionParser(ILoggerFactory loggerFactory, ApplicationSettings settings, FrameDefinitionProcessorService frameDefinitionProcessorService, TypeMapperService typeMapperService)
        {
            _logger = loggerFactory.CreateLogger<EZSPYAMLDefinitionParser>();
            _settings = settings;
            _frameDefinitionProcessorService = frameDefinitionProcessorService;
            _tyepMapperService = typeMapperService;
        }

        public void Process(string versionDir, string definitionPath, string versionName)
        {
            _versionName = versionName;

            // Read numeric constants from header file if they exist
            foreach (var headerFile in Directory.GetFiles(versionDir, "*.h"))
            {
                _logger.LogInformation("Processing C constants from {file}", headerFile);

                string[] lines = File.ReadAllLines(headerFile);
                foreach (string line in lines)
                {
                    var match = Regex.Match(line, @"#define\s+(?<name>[A-Za-z_0-9]+)\s+(?<value>[A-Za-z_0-9]+)");
                    if (match.Success)
                    {
                        var name = match.Groups["name"].Value.Trim();
                        var value = match.Groups["value"].Value.Trim();
                        //if (int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int intValue))
                        //{
                        //    value = intValue.ToString();
                        //}
                        //else if (value.StartsWith("0x") && int.TryParse(value[2..], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int hexValue))
                        //{
                        //    value = hexValue.ToString();
                        //}
                        MapCConstants.AddConstantMapping(name, value);
                        _logger.LogInformation("Mapped C constant: {name} = {value})", name, value);
                    }
                }
            }


            try
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .WithTypeConverter(new TypedefDefinitionConverter())
                    .Build();

                using (TextReader definitionFileReader = new StreamReader(definitionPath))
                {
                    var sections = deserializer.Deserialize<List<EzspSection>>(definitionFileReader);

                    if (sections == null || sections.Count == 0)
                    {
                        _logger.LogWarning("No sections found in {file}", definitionPath);
                        return;
                    }

                    _logger.LogInformation("Successfully deserialized {count} section(s) from {version}", sections.Count, versionName);

                    Dictionary<string, string> frameNumberResponses = new Dictionary<string, string>();
                    List<string> frameNamespaces = new List<string>();

                    foreach (var section in sections)
                    {
                        _logger.LogInformation("\tSection: {section}", section.Name);

                        string sanitizedSectionName = Sanitize.SectionName(section.Name);

                        #region // simple types
                        var simpleCount = 0;
                        var complexCount = 0;

                        if (section.Typedefs != null && section.Typedefs.Any(x => x.Definition is SimpleTypedefDefinition))
                        {

                            foreach (var typedef in section.Typedefs.Where(x => x.Definition is SimpleTypedefDefinition))
                            {
                                simpleCount++;

                                string content = SimpleTypeDefinitionProcessor.ProcessSimpleTypeDefinition(_logger, typedef, definitionPath.Substring(0, definitionPath.LastIndexOf('\\')), sanitizedSectionName);
                                if (string.IsNullOrWhiteSpace(content) == false)
                                {
                                    SaveEnumFile(section.Name, Sanitize.EnumerationName(typedef.Name), content);
                                }
                            }
                        }

                        #endregion

                        // Structs (complex types) might contain enums, so process enums first
                        #region // Enums
                        if (section.Enums != null && section.Enums.Count > 0)
                        {
                            _logger.LogInformation("    Found {count} enum(s)", section.Enums.Count);
                            foreach (var enumDef in section.Enums)
                            {
                                _logger.LogInformation("      - {name} ({type}): {itemCount} items", enumDef.Name, enumDef.Type, enumDef.Items?.Count ?? 0);
                                TypeMapping? enumBaseTypeMapping = _tyepMapperService.GetTypeMapping(enumDef.Type);

                                if (enumBaseTypeMapping is not null)
                                {
                                    _tyepMapperService.AddTypeMapping(
                                        new CType(enumDef.Name, enumBaseTypeMapping.Value.CType.SizeInBytes, enumDef.Description)
                                        {
                                            IsEnum = true,
                                            UnderlyingTypeName = enumBaseTypeMapping.Value.CType.Name
                                        },
                                        new CSharpType(Sanitize.EnumerationName(enumDef.Name))
                                        {
                                            IsEnum = true,
                                            UnderlyingTypeName = enumBaseTypeMapping.Value.CSharpType.Name,
                                        }
                                    );

                                    string enumFileContent = EnumDefinitionProcessor.ProcessEnumDefinition(_logger, section.Name, enumDef);
                                    if (string.IsNullOrWhiteSpace(enumFileContent))
                                    {
                                        _logger.LogWarning("Enum file content is empty for {enum} in section {section}", enumDef.Name, section.Name);
                                        continue;
                                    }
                                    SaveEnumFile(section.Name, Sanitize.EnumerationName(enumDef.Name), enumFileContent); 
                                }
                                else
                                {
                                    _logger.LogError("No type mapping found for enum base type {type} of enum {enum} in section {section}, cannot finish", enumDef.Type, enumDef.Name, section.Name);
                                    return;
                                }


                            }
                        }
                        #endregion

                        #region // complex types
                        if (section.Typedefs != null && section.Typedefs.Any(x => x.Definition is ComplexTypedefDefinition))
                        {
                            foreach (var typedef in section.Typedefs.Where(x => x.Definition is ComplexTypedefDefinition))
                            {
                                complexCount++;

                                string complexTypeContent = ComplexTypeDefinitionProcessor.ProcessComplexTypeDefinition(_logger, sanitizedSectionName, typedef);

                                SaveComplexTypeFile(section.Name, Sanitize.StructureName(typedef.Name), complexTypeContent);
                            }
                        }
                        #endregion

                        _logger.LogInformation("\t\tFound {count} typedef(s) - {simple} simple, {complex} complex",
                            section.Typedefs?.Count ?? 0, simpleCount, complexCount);

                        #region // Frames
                        if (section.Frames != null && section.Frames.Count > 0)
                        {
                            frameNamespaces.Add($"ZigBeeNet.Hardware.EmberV8Plus.Ezsp.{Sanitize.SectionName(section.Name)}.Frames;");

                            _logger.LogInformation("    Found {count} frame(s)", section.Frames.Count);

                            foreach (var frameDefinition in section.Frames)
                            {
                                _logger.LogInformation("      - {name} ({value}): {cmdArgs} cmd args, {respArgs} resp args",
                                    frameDefinition.CommandName, frameDefinition.Value,
                                    frameDefinition.CommandArguments?.Count ?? 0,
                                    frameDefinition.ResponseArguments?.Count ?? 0);

                                string frameRequestContent = _frameDefinitionProcessorService.ProcessFrameDefinitionForRequest(section.Name, frameDefinition);
                                string frameResponseContent = _frameDefinitionProcessorService.ProcessFrameDefinitionForResponse(section.Name, frameDefinition);

                                string className = char.ToUpper(frameDefinition.CommandName[0]) + frameDefinition.CommandName[1..];
                                string classNameResponse = className + "Response";

                                if (string.IsNullOrWhiteSpace(frameRequestContent) == false)
                                {
                                    SaveFrameFile(section.Name, $"{className}Request", frameRequestContent);
                                }

                                if (string.IsNullOrWhiteSpace(frameResponseContent) == false)
                                {
                                    SaveFrameFile(section.Name, classNameResponse, frameResponseContent);
                                }

                                frameNumberResponses.Add(frameDefinition.Value, classNameResponse);
                            }
                        }
                        #endregion
                    }


                    StringBuilder EzspFrameResponseV8PlusAutoGeneratedContent = new StringBuilder();
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("//------------------------------------------------------------------------------");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("// <auto-generated>");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("//     This code was generated by a tool.");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("//");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("//     the code is regenerated.");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("// </auto-generated>");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("//------------------------------------------------------------------------------");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine();
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("using System;");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("using System.Collections.Generic;");
                    foreach (string frameNamespace in frameNamespaces)
                    {
                        EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine($"using {frameNamespace}");
                    }

                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp;");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine();
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("public abstract partial class EzspFrameResponseV8Plus");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("{");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("\tprivate static System.Collections.Generic.Dictionary<int, Func<byte[], EzspFrameResponseV8Plus>> _ezspHandlerDict = new Dictionary<int, Func<byte[], EzspFrameResponseV8Plus>>() {");
                    foreach (KeyValuePair<string, string> frameNumberResponse in frameNumberResponses.OrderBy(x => int.Parse(x.Key[2..], System.Globalization.NumberStyles.HexNumber)))
                    {
                        EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine($"\t\t{{ {frameNumberResponse.Key}, (frameBytes) => {frameNumberResponse.Value}.Parse(frameBytes) }},");
                        _logger.LogDebug("Frame Number {number} => Response Handler: {handler}", frameNumberResponse.Key, frameNumberResponse.Value);

                    }
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("\t};");
                    EzspFrameResponseV8PlusAutoGeneratedContent.AppendLine("}");

                    SaveFile("", "", "EzspFrameResponseV8PlusAutoGenerated", EzspFrameResponseV8PlusAutoGeneratedContent.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing version {version}", versionName);
            }
        }


        private void SaveComplexTypeFile(string sectionName, string typeName, string content)
        {
            SaveFile(sectionName, "Types", typeName, content);
        }

        private void SaveEnumFile(string sectionName, string enumName, string content)
        {
            SaveFile(sectionName, "Enumerations", enumName, content);
        }

        private void SaveFrameFile(string sectionName, string frameName, string content)
        {
            SaveFile(sectionName, "Frames", frameName, content);
        }

        private void SaveFile(string sectionName, string subSection, string fileName, string fileContent)
        {
            string sanitizedSectionName = Sanitize.SectionName(sectionName);
            string outputPath = Path.Combine(_settings.OutputDirectory, _versionName, sanitizedSectionName, subSection);
            Directory.CreateDirectory(outputPath);

            File.WriteAllText(Path.Combine(outputPath, $"{fileName}.cs"), $"#if VERSION_{_versionName.Replace('.', '_')}\r\n{fileContent}\r\n#endif");
        }




    }
}
