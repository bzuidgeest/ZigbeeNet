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
using Microsoft.Extensions.DependencyInjection;


namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Parser
{
    internal class EZSPYAMLDefinitionParser
    {
        private readonly ILogger<EZSPYAMLDefinitionParser> _logger;
        private readonly ApplicationSettings _settings;
        private string _versionName = string.Empty;
        private readonly TypeMapperService _typeMapperService;
        private readonly IServiceProvider _serviceProvider;

        public EZSPYAMLDefinitionParser(ILoggerFactory loggerFactory, IServiceProvider serviceProvider, ApplicationSettings settings, TypeMapperService typeMapperService)
        {
            _logger = loggerFactory.CreateLogger<EZSPYAMLDefinitionParser>();
            _settings = settings;
            _typeMapperService = typeMapperService;
            _serviceProvider = serviceProvider;
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

                    // add some other common constants
                    ;
                    MapCConstants.AddConstantMapping("SL_ZIGBEE_COUNTER_TYPE_COUNT", sections.Single(x => x.Name == "Common").Enums.Single(x => x.Name == "sl_zigbee_counter_type_t").Items.Single(x => x.Name == "SL_ZIGBEE_COUNTER_TYPE_COUNT").Value);

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

                        SimpleTypeDefinitionProcessorService simpleTypeDefinitionProcessorService = _serviceProvider.GetRequiredService<SimpleTypeDefinitionProcessorService>();
                        EnumDefinitionProcessorService enumDefinitionProcessorService = _serviceProvider.GetRequiredService<EnumDefinitionProcessorService>();
                        ComplexTypeDefinitionProcessorService complexTypeDefinitionProcessorService = _serviceProvider.GetRequiredService<ComplexTypeDefinitionProcessorService>();
                        FrameDefinitionProcessorService frameDefinitionProcessorService = _serviceProvider.GetRequiredService<FrameDefinitionProcessorService>();

                        string sanitizedSectionName = Sanitize.SectionName(section.Name);

                        #region // simple types
                        var simpleCount = 0;
                        var complexCount = 0;

                        if (section.Typedefs != null)
                        {
                            foreach (var typedef in section.Typedefs.Where(x => x.IsSimple))
                            {
                                simpleCount++;

                                simpleTypeDefinitionProcessorService.Process(sanitizedSectionName, typedef);
                            }
                        }

                        #endregion

                        // Structs (complex types) might contain enums, so process enums first
                        #region // Enums
                        if (section.Enums != null && section.Enums.Count > 0)
                        {
                            _logger.LogInformation("\t\tFound {count} enum(s)", section.Enums.Count);
                            foreach (var enumDef in section.Enums)
                            {
                                enumDefinitionProcessorService.Process(sanitizedSectionName, enumDef);
                            }
                        }
                        #endregion

                        #region // complex types
                        if (section.Typedefs != null && section.Typedefs.Any(x => x.Definition is ComplexTypedefDefinition))
                        {
                            foreach (var typedef in section.Typedefs.Where(x => x.Definition is ComplexTypedefDefinition))
                            {
                                complexCount++;
                                ComplexTypedefDefinition complexTypedefDefinition = (ComplexTypedefDefinition)typedef.Definition;

                                complexTypeDefinitionProcessorService.ProcessComplexTypeDefinition(section.Name, typedef);
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

                                string frameRequestContent = frameDefinitionProcessorService.ProcessFrameDefinitionForRequest(section.Name, frameDefinition);
                                string frameResponseContent = frameDefinitionProcessorService.ProcessFrameDefinitionForResponse(section.Name, frameDefinition);

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


                    #region // EzspFrameResponseV8PlusAutoGenerated.cs

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

                    #endregion

                    #region // EmberNCP.cs

                    _logger.LogInformation("Generating EmberNCPAutoGenerated.cs");

                    StringBuilder emberNCPAutoGeneratedContent = new StringBuilder();
                    emberNCPAutoGeneratedContent.AppendLine("//------------------------------------------------------------------------------");
                    emberNCPAutoGeneratedContent.AppendLine("// <auto-generated>");
                    emberNCPAutoGeneratedContent.AppendLine("//     This code was generated by a tool.");
                    emberNCPAutoGeneratedContent.AppendLine("//");
                    emberNCPAutoGeneratedContent.AppendLine("//     Changes to this file may cause incorrect behavior and will be lost if");
                    emberNCPAutoGeneratedContent.AppendLine("//     the code is regenerated.");
                    emberNCPAutoGeneratedContent.AppendLine("// </auto-generated>");
                    emberNCPAutoGeneratedContent.AppendLine("//------------------------------------------------------------------------------");
                    emberNCPAutoGeneratedContent.AppendLine();
                    emberNCPAutoGeneratedContent.AppendLine("using System;");
                    emberNCPAutoGeneratedContent.AppendLine("using System.Collections.Generic;");
                    foreach (string frameNamespace in frameNamespaces)
                    {
                        emberNCPAutoGeneratedContent.AppendLine($"using {frameNamespace}");
                    }

                    emberNCPAutoGeneratedContent.AppendLine("using ZigBeeNet.Hardware.EmberV8Plus.Transaction;");
                    emberNCPAutoGeneratedContent.AppendLine("using ZigBeeNet.Hardware.EmberV8Plus.Internal;");
                    emberNCPAutoGeneratedContent.AppendLine("using ZigBeeNet.Hardware.EmberV8Plus.Ezsp.Common.Enumerations;");
                    emberNCPAutoGeneratedContent.AppendLine("using Microsoft.Extensions.Logging;");
                    emberNCPAutoGeneratedContent.AppendLine("using ZigBeeNet.Util;");

                    emberNCPAutoGeneratedContent.AppendLine("namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp;");
                    emberNCPAutoGeneratedContent.AppendLine();
                    emberNCPAutoGeneratedContent.AppendLine("public partial class EmberNcp");
                    emberNCPAutoGeneratedContent.AppendLine("{");

                    emberNCPAutoGeneratedContent.AppendLine("\t\tstatic private readonly ILogger _logger = LogManager.GetLog<EmberNcp>();");
                    emberNCPAutoGeneratedContent.AppendLine();
                    emberNCPAutoGeneratedContent.AppendLine("\t\t/**");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t * The protocol handler used to send and receive EZSP packets");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t */");
                    emberNCPAutoGeneratedContent.AppendLine("\t\tprivate IEzspProtocolHandler _protocolHandler;");
                    emberNCPAutoGeneratedContent.AppendLine();
                    emberNCPAutoGeneratedContent.AppendLine("\t\t/**");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t * The status value from the last request");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t */");
                    emberNCPAutoGeneratedContent.AppendLine("\t\tprivate ZigbeeEzspStatus _lastStatus;");
                    emberNCPAutoGeneratedContent.AppendLine();
                    emberNCPAutoGeneratedContent.AppendLine("\t\t/**");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t * Create the NCP instance");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t *");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t * @param protocolHandler the {@link EzspFrameHandler} used for communicating with the NCP");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t */");
                    emberNCPAutoGeneratedContent.AppendLine("\t\tpublic EmberNcp(IEzspProtocolHandler protocolHandler)");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t{");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t\tthis._protocolHandler = protocolHandler;");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t}");
                    emberNCPAutoGeneratedContent.AppendLine();
                    emberNCPAutoGeneratedContent.AppendLine("\t\t/**");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t * Returns the {@link EmberStatus} from the last request. If the request did not provide a status, null is returned.");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t *");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t * @return {@link EmberStatus}");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t */");
                    emberNCPAutoGeneratedContent.AppendLine("\t\tpublic ZigbeeEzspStatus GetLastStatus()");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t{");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t\treturn _lastStatus;");
                    emberNCPAutoGeneratedContent.AppendLine("\t\t}");
                    foreach (FrameDefinition frameDefinition in sections.SelectMany(x => x.Frames ?? new List<FrameDefinition>()))
                    {
                        string sanitizedCommandName = Sanitize.FunctionName(frameDefinition.CommandName);

                        emberNCPAutoGeneratedContent.AppendLine($"\t\t");

                        TextHelper.AppendBasicSummary(emberNCPAutoGeneratedContent, frameDefinition.Description, 2);

                        emberNCPAutoGeneratedContent.Append($"\t\tpublic {sanitizedCommandName}Response {sanitizedCommandName}(");

                        string inputParameters = String.Join(", ", frameDefinition.CommandArguments.Select(arg => {
                            TypeMapping? argType = _typeMapperService.GetTypeMapping(arg.Type);
                            return $"{argType?.CSharpType.Name} {arg.Name}";
                        }));

                        emberNCPAutoGeneratedContent.Append(inputParameters);

                        emberNCPAutoGeneratedContent.AppendLine(")");
                        emberNCPAutoGeneratedContent.AppendLine("\t\t{");

                        emberNCPAutoGeneratedContent.AppendLine($"\t\t\t{sanitizedCommandName}Request request = new {sanitizedCommandName}Request();");
                        emberNCPAutoGeneratedContent.AppendLine("\t\t\tIEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(NetworkInitResponse)));");
                        emberNCPAutoGeneratedContent.AppendLine($"\t\t\t{sanitizedCommandName}Response response = ({sanitizedCommandName}Response)transaction.GetResponse();");
                        emberNCPAutoGeneratedContent.AppendLine("\t\t\t_logger.LogDebug(response.ToString());");
                        
                        emberNCPAutoGeneratedContent.AppendLine("\t\t\treturn response;");

                        emberNCPAutoGeneratedContent.AppendLine("\t\t}");
                        //                 /**
                        // * Resume network operation after a reboot. The node retains its original type. This should be called on startup
                        // * whether or not the node was previously part of a network. EMBER_NOT_JOINED is returned if the node is not part of
                        // * a network.
                        // *
                        // * @return {@link EmberStatus} if success or failure
                        // */
                        //public EmberStatus NetworkInit()
                        //{
                        //    NetworkInitRequest request = new NetworkInitRequest();
                        //    IEzspTransaction transaction = _protocolHandler.SendEzspTransaction(new EzspSingleResponseTransaction(request, typeof(EzspNetworkInitResponse)));
                        //    NetworkInitResponse response = (NetworkInitResponse)transaction.GetResponse();
                        //    _logger.LogDebug(response.ToString());

                        //    return response.GetStatus();
                        //}

                    }
                    emberNCPAutoGeneratedContent.AppendLine("\t};");
                    emberNCPAutoGeneratedContent.AppendLine("}");

                    SaveFile("", "", "EmberNCPAutoGenerated", emberNCPAutoGeneratedContent.ToString());

                    #endregion
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
