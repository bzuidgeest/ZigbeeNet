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

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Parser
{
    internal class EZSPYAMLDefinitionParser
    {
        private readonly ILogger<EZSPYAMLDefinitionParser> _logger;
        private readonly ApplicationSettings _settings;

        public EZSPYAMLDefinitionParser(ILoggerFactory loggerFactory, ApplicationSettings settings)
        {
            _logger = loggerFactory.CreateLogger<EZSPYAMLDefinitionParser>();
            _settings = settings;
        }

        public void Process(string versionDir, string definitionPath, string versionName)
        {
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
                                string enumFileContent = EnumDefinitionProcessor.ProcessEnumDefinition(_logger, section.Name, enumDef);
                                if (string.IsNullOrWhiteSpace(enumFileContent))
                                {
                                    _logger.LogWarning("Enum file content is empty for {enum} in section {section}", enumDef.Name, section.Name);
                                    continue;
                                }
                                SaveEnumFile(section.Name, Sanitize.EnumerationName(enumDef.Name), enumFileContent);
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

                                string frameRequestContent = FrameDefinitionProcessor.ProcessFrameDefinitionForRequest(_logger, section.Name, frameDefinition);
                                string frameResponseContent = FrameDefinitionProcessor.ProcessFrameDefinitionForResponse(_logger, section.Name, frameDefinition);

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

                    SaveFile(Path.Combine(_settings.OutputDirectory, "EzspFrameResponseV8PlusAutoGenerated.cs"), EzspFrameResponseV8PlusAutoGeneratedContent.ToString());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing version {version}", versionName);
            }
        }


        private void SaveComplexTypeFile(string sectionName, string typeName, string content)
        {
            // Create output directory for this section
            string sanitizedSectionName = Sanitize.SectionName(sectionName);
            string outputDir = Path.Combine(_settings.OutputDirectory, sanitizedSectionName, "Types");
            Directory.CreateDirectory(outputDir);

            string fileName = $"{typeName}.cs";
            string filePath = Path.Combine(outputDir, fileName);

            File.WriteAllText(filePath, content);
        }

        private void SaveEnumFile(string sectionName, string enumName, string content)
        {
            string sanitizedSectionName = Sanitize.SectionName(sectionName);
            string outputDir = Path.Combine(_settings.OutputDirectory, sanitizedSectionName, "Enumerations");
            Directory.CreateDirectory(outputDir);

            string fileName = $"{enumName}.cs";
            string filePath = Path.Combine(outputDir, fileName);

            File.WriteAllText(filePath, content);
        }

        private void SaveFrameFile(string sectionName, string frameName, string content)
        {
            string sanitizedSectionName = Sanitize.SectionName(sectionName).Replace("Frames", "");
            string outputDir = Path.Combine(_settings.OutputDirectory, sanitizedSectionName, "Frames");
            Directory.CreateDirectory(outputDir);

            string fileName = $"{frameName}.cs";
            string filePath = Path.Combine(outputDir, fileName);

            File.WriteAllText(filePath, content);
        }

        private void SaveFile(string filename, string fileContent)
        {
            File.WriteAllText(filename, fileContent);
        }




    }
}
