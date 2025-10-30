using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using ZigBeeNet.Ember.CodeGeneratorV8Plus.Utility;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Models;

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
                        _logger.LogInformation("  Section: {section}", section.Section);

                        #region // types
                        if (section.Typedefs != null && section.Typedefs.Count > 0)
                        {
                            var simpleTypedefs = new List<Typedef>();
                            var simpleCount = 0;
                            var complexCount = 0;

                            foreach (var typedef in section.Typedefs)
                            {
                                if (typedef.Definition is SimpleTypedefDefinition simple)
                                {
                                    simpleCount++;
                                    simpleTypedefs.Add(typedef);
                                    _logger.LogInformation("      - {name} (simple): {type}", typedef.Name, simple.Type);
                                }
                                else if (typedef.Definition is ComplexTypedefDefinition complex)
                                {
                                    complexCount++;
                                    _logger.LogInformation("      - {name} (complex): {fieldCount} fields", typedef.Name, complex.Fields.Count);
                                    ProcessComplexTypeDefinition(section.Section, typedef);
                                }
                            }

                            // Process all simple typedefs for this section at once
                            if (simpleTypedefs.Count > 0)
                            {
                                ProcessSimpleTypeDefinitions(section.Section, simpleTypedefs);
                            }

                            _logger.LogInformation("    Found {count} typedef(s) - {simple} simple, {complex} complex",
                                section.Typedefs.Count, simpleCount, complexCount);
                        }
                        #endregion

                        #region // Enums
                        if (section.Enums != null && section.Enums.Count > 0)
                        {
                            _logger.LogInformation("    Found {count} enum(s)", section.Enums.Count);
                            foreach (var enumDef in section.Enums)
                            {
                                _logger.LogInformation("      - {name} ({type}): {itemCount} items", enumDef.Name, enumDef.Type, enumDef.Items?.Count ?? 0);
                                string enumFileContent = EnumDefinitionProcessor.ProcessEnumDefinition(_logger, section.Section, enumDef);
                                if (string.IsNullOrWhiteSpace(enumFileContent))
                                {
                                    _logger.LogWarning("Enum file content is empty for {enum} in section {section}", enumDef.Name, section.Section);
                                    continue;
                                }
                                SaveEnumFile(section.Section, enumDef.Name, enumFileContent);
                            }
                        }
                        #endregion

                        #region // Frames
                        if (section.Frames != null && section.Frames.Count > 0)
                        {
                            frameNamespaces.Add($"ZigBeeNet.Hardware.EmberV8Plus.Ezsp.{Sanitize.SectionName(section.Section)}.Frames;");

                            _logger.LogInformation("    Found {count} frame(s)", section.Frames.Count);
                            
                            foreach (var frameDefinition in section.Frames)
                            {
                                _logger.LogInformation("      - {name} ({value}): {cmdArgs} cmd args, {respArgs} resp args",
                                    frameDefinition.CommandName, frameDefinition.Value,
                                    frameDefinition.CommandArguments?.Count ?? 0,
                                    frameDefinition.ResponseArguments?.Count ?? 0);

                                string frameRequestContent = FrameDefinitionProcessor.ProcessFrameDefinitionForRequest(_logger, section.Section, frameDefinition);
                                string frameResponseContent = FrameDefinitionProcessor.ProcessFrameDefinitionForResponse(_logger, section.Section, frameDefinition);

                                string className = char.ToUpper(frameDefinition.CommandName[0]) + frameDefinition.CommandName[1..];
                                string classNameResponse = className + "Response";

                                if (string.IsNullOrWhiteSpace(frameRequestContent) == false)
                                {
                                    SaveFrameFile(section.Section, $"{className}Request", frameRequestContent);
                                }

                                if (string.IsNullOrWhiteSpace(frameResponseContent) == false)
                                {
                                    SaveFrameFile(section.Section, classNameResponse, frameResponseContent);
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

        private void ProcessSimpleTypeDefinitions(string sectionName, List<Typedef> typedefs)
        {
            try
            {
                if (typedefs == null || typedefs.Count == 0)
                {
                    return;
                }

                // Create output directory for this section
                string projectDir = Directory.GetCurrentDirectory();
                string outputDir = Path.Combine(_settings.OutputDirectory, Sanitize.SectionName(sectionName));
                Directory.CreateDirectory(outputDir);

                // Create a file for type aliases
                string fileName = "TypeAliases.cs";
                string filePath = Path.Combine(outputDir, fileName);

                // Generate the complete file with all typedefs
                var content = GenerateTypeAliasFile(sectionName, typedefs);
                File.WriteAllText(filePath, content);
                _logger.LogDebug("Created type alias file with {count} aliases: {path}", typedefs.Count, filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing simple typedefs for section {section}", sectionName);
            }
        }
        private void ProcessComplexTypeDefinition(string sectionName, Typedef typedef)
        {
            try
            {
                if (typedef?.Definition is not ComplexTypedefDefinition complex)
                {
                    return;
                }

                // Create output directory for this section
                string sanitizedSectionName = Sanitize.SectionName(sectionName);
                string outputDir = Path.Combine(_settings.OutputDirectory, sanitizedSectionName, "Types");
                Directory.CreateDirectory(outputDir);

                // Create a separate file for each struct
                string fileName = $"{typedef.Name}.cs";
                string filePath = Path.Combine(outputDir, fileName);

                // Generate the struct definition
                var content = GenerateComplexTypeDefinition(typedef, complex);

                // Create file with namespace wrapper using section name
                var fileContent = new StringBuilder();
                fileContent.AppendLine($"namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.{sanitizedSectionName}.Types;");
                fileContent.AppendLine();
                fileContent.Append(content);
                File.WriteAllText(filePath, fileContent.ToString());

                _logger.LogDebug("Created complex type definition: {path}", filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing complex typedef {name} for section {section}", typedef?.Name, sectionName);
            }
        }

        private static string GenerateTypeAliasFile(string sectionName, List<Typedef> typedefs)
        {
            var sb = new StringBuilder();

            // No namespace declaration - global using directives must be at file level

            foreach (var typedef in typedefs)
            {
                if (typedef.Definition is SimpleTypedefDefinition simpleTypedefDefinition)
                {
                    if (!string.IsNullOrEmpty(typedef.Description))
                    {
                        sb.AppendLine($"/// <summary>");
                        sb.AppendLine($"/// {typedef.Description}");
                        sb.AppendLine($"/// </summary>");
                    }

                    // Check if this is an array type
                    int bracketIndex = simpleTypedefDefinition.Type.IndexOf('[');
                    if (bracketIndex >= 0)
                    {
                        if (typedef.Name.Contains("string"))
                        {
                            // (array types cannot be used in type aliases, see if they work as string....)");
                            sb.AppendLine($"/// <remarks>Original C type: {simpleTypedefDefinition.Type}</remarks>");
                            sb.AppendLine($"global using {typedef.Name} = string;");
                        }
                        else
                        {
                            // Array type - cannot use in alias, skip it
                            sb.AppendLine($"/// <remarks>Original C type: {simpleTypedefDefinition.Type} (array types cannot be used in type aliases)</remarks>");
                            sb.AppendLine($"// Skipped: {typedef.Name}");
                        }
                    }
                    else
                    {
                        // Simple scalar type - use global using alias
                        string csharpType = MapCTypes.MapBaseCType(simpleTypedefDefinition.Type);
                        sb.AppendLine($"/// <remarks>Original C type: {simpleTypedefDefinition.Type}</remarks>");
                        sb.AppendLine($"global using {typedef.Name} = {csharpType};");
                    }
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private static string GenerateComplexTypeDefinition(Typedef typedef, ComplexTypedefDefinition complex)
        {
            var sb = new StringBuilder();

            // Add XML documentation
            if (!string.IsNullOrEmpty(typedef.Description))
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// {typedef.Description}");
                sb.AppendLine("/// </summary>");
            }

            // Generate struct definition
            sb.AppendLine($"public struct {typedef.Name}");
            sb.AppendLine("{");

            // Add fields
            foreach (var field in complex.Fields)
            {
                if (!string.IsNullOrEmpty(field.Description))
                {
                    sb.AppendLine("    /// <summary>");
                    sb.AppendLine($"    /// {field.Description}");
                    sb.AppendLine("    /// </summary>");
                }

                // Check if the field name contains array notation (e.g., fieldName[SIZE])
                int nameBracketIndex = field.Name?.IndexOf('[') ?? -1;
                if (nameBracketIndex >= 0)
                {
                    // Extract field name and array size from name
                    string fieldName = field.Name![..nameBracketIndex];
                    string arrayPart = field.Name[nameBracketIndex..];
                    string arraySize = arrayPart.Trim('[', ']');
                    string csharpBaseType = MapCTypes.MapBaseCType(field.Type);

                    // Check if array size is numeric (compile-time constant)
                    if (int.TryParse(arraySize, out _))
                    {
                        // Use fixed keyword for fixed-size arrays with numeric constants
                        sb.AppendLine($"    public fixed {csharpBaseType} {fieldName}[{arraySize}];");
                    }
                    else
                    {
                        // Array size is a symbolic constant - use a comment and skip
                        sb.AppendLine($"    // Array field with symbolic size: {arraySize}");
                        sb.AppendLine($"    // public fixed {csharpBaseType} {fieldName}[{arraySize}];");
                    }
                }
                // Check if the type contains array notation (e.g., uint8_t[8])
                else if (field.Type?.IndexOf('[') >= 0)
                {
                    int typeBracketIndex = field.Type.IndexOf('[');
                    // Extract base type and array size
                    string baseType = field.Type[..typeBracketIndex];
                    string arrayPart = field.Type[typeBracketIndex..];
                    string csharpBaseType = MapCTypes.MapBaseCType(baseType);

                    // Extract array size from [N] format
                    string arraySize = arrayPart.Trim('[', ']');

                    // Check if array size is numeric (compile-time constant)
                    if (int.TryParse(arraySize, out _))
                    {
                        // Use fixed keyword for fixed-size arrays with numeric constants
                        sb.AppendLine($"    public fixed {csharpBaseType} {field.Name}[{arraySize}];");
                    }
                    else
                    {
                        // Array size is a symbolic constant - use a comment and skip
                        sb.AppendLine($"    // Array field with symbolic size: {arraySize}");
                        sb.AppendLine($"    // public fixed {csharpBaseType} {field.Name}[{arraySize}];");
                    }
                }
                else
                {
                    // Simple scalar type
                    string csharpType = MapCTypes.MapBaseCType(field.Type);
                    sb.AppendLine($"    public {csharpType} {field.Name};");
                }
                sb.AppendLine();
            }

            sb.AppendLine("}");
            sb.AppendLine();

            return sb.ToString();
        }

    }
}
