using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.Ember.CodeGeneratorV8Plus.Utility;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Models;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Parser
{
    internal static class FrameDefinitionProcessor
    {
        /// <summary>
        /// Processes a frame definition and generates C# request class code.
        /// </summary>
        /// <param name="logger">Logger instance for diagnostic output.</param>
        /// <param name="section">The section name (used for namespace generation).</param>
        /// <param name="frameDefinition">The frame definition to process.</param>
        /// <returns>Generated C# request class code as a string.</returns>
        internal static string ProcessFrameDefinitionForRequest(ILogger<EZSPYAMLDefinitionParser> logger, string? section, FrameDefinition frameDefinition)
        {
            try
            {
                if (frameDefinition == null)
                {
                    logger.LogWarning("Frame definition is null");
                    return string.Empty;
                }

                if (string.IsNullOrWhiteSpace(frameDefinition.CommandName))
                {
                    logger.LogWarning("Frame command name is empty");
                    return string.Empty;
                }

                var sb = new StringBuilder();

                sb.AppendLine("using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;");
                sb.AppendLine();

                // Generate namespace
                string sanitizedSectionName = Sanitize.SectionName(section);
                sb.AppendLine($"namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.{sanitizedSectionName}.Frames;");
                sb.AppendLine();

                // Generate XML documentation with frame value
                if (!string.IsNullOrWhiteSpace(frameDefinition.Description))
                {
                    sb.AppendLine("/// <summary>");
                    sb.AppendLine($"/// {frameDefinition.Description.XmlEscape().AddXmlCommentPrefixAfterLineBreak(1)}");
                    if (!string.IsNullOrWhiteSpace(frameDefinition.Value))
                    {
                        sb.AppendLine($"/// Frame value: {frameDefinition.Value}");
                    }
                    sb.AppendLine("/// </summary>");
                }

                // Generate class declaration inheriting from EzspFrameRequest
                string className = char.ToUpper(frameDefinition.CommandName[0]) + frameDefinition.CommandName[1..] + "Request";
                sb.AppendLine($"public class {className} : EzspFrameRequest");
                sb.AppendLine("{");

                // Generate properties for command arguments
                if (frameDefinition.CommandArguments != null && frameDefinition.CommandArguments.Count > 0)
                {
                    foreach (var arg in frameDefinition.CommandArguments)
                    {
                        if (!string.IsNullOrWhiteSpace(arg.Description))
                        {
                            sb.AppendLine("    /// <summary>");
                            sb.AppendLine($"    /// {arg.Description.XmlEscape().AddXmlCommentPrefixAfterLineBreak(1)}");
                            sb.AppendLine("    /// </summary>");
                        }

                        string csharpType = MapCTypes.MapBaseCType(arg.Type);
                        sb.AppendLine($"    public {csharpType} {arg.Name} {{ get; set; }}");
                        sb.AppendLine();
                    }
                }

                logger.LogDebug("Generated request class {commandName} with {argCount} arguments",
                    className, frameDefinition.CommandArguments?.Count ?? 0);

                return sb.ToString();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing frame definition for request {commandName}",
                    frameDefinition?.CommandName ?? "unknown");
                return string.Empty;
            }
        }

        /// <summary>
        /// Processes a frame definition and generates C# response class code.
        /// </summary>
        /// <param name="logger">Logger instance for diagnostic output.</param>
        /// <param name="section">The section name (used for namespace generation).</param>
        /// <param name="frameDefinition">The frame definition to process.</param>
        /// <returns>Generated C# response class code as a string.</returns>
        internal static string ProcessFrameDefinitionForResponse(ILogger<EZSPYAMLDefinitionParser> logger, string? section, FrameDefinition frameDefinition)
        {
            try
            {
                if (frameDefinition == null)
                {
                    logger.LogWarning("Frame definition is null");
                    return string.Empty;
                }

                if (string.IsNullOrWhiteSpace(frameDefinition.CommandName))
                {
                    logger.LogWarning("Frame command name is empty");
                    return string.Empty;
                }

                var sb = new StringBuilder();

                sb.AppendLine("using ZigBeeNet.Hardware.EmberV8Plus.Ezsp;");
                sb.AppendLine();

                // Generate namespace
                string sanitizedSectionName = Sanitize.SectionName(section);
                sb.AppendLine($"namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.{sanitizedSectionName}.Frames;");
                sb.AppendLine();

                // Generate XML documentation with frame value
                if (!string.IsNullOrWhiteSpace(frameDefinition.Description))
                {
                    sb.AppendLine("/// <summary>");
                    sb.AppendLine($"/// {frameDefinition.Description.XmlEscape().AddXmlCommentPrefixAfterLineBreak(1)}");
                    if (!string.IsNullOrWhiteSpace(frameDefinition.Value))
                    {
                        sb.AppendLine($"/// Frame value: {frameDefinition.Value}");
                    }
                    sb.AppendLine("/// </summary>");
                }

                string className = char.ToUpper(frameDefinition.CommandName[0]) + frameDefinition.CommandName[1..] + "Response";
                // Generate class declaration inheriting from EzspFrameResponse
                sb.AppendLine($"public class {className} : EzspFrameResponse");
                sb.AppendLine("{");

                // Generate properties for response arguments
                if (frameDefinition.ResponseArguments != null && frameDefinition.ResponseArguments.Count > 0)
                {
                    foreach (var arg in frameDefinition.ResponseArguments)
                    {
                        if (!string.IsNullOrWhiteSpace(arg.Description))
                        {
                            sb.AppendLine("    /// <summary>");
                            sb.AppendLine($"    /// {arg.Description.XmlEscape().AddXmlCommentPrefixAfterLineBreak(1)}");
                            sb.AppendLine("    /// </summary>");
                        }

                        string csharpType = MapCTypes.MapBaseCType(arg.Type);
                        sb.AppendLine($"    public {csharpType} {arg.Name} {{ get; set; }}");
                        sb.AppendLine();
                    }
                }

                sb.AppendLine("\tParse(byte[] frameBytes)");
                sb.AppendLine("\t{");
                sb.AppendLine("\t\t// TODO: Implement parsing logic");
                if (frameDefinition.ResponseArguments != null && frameDefinition.ResponseArguments.Count > 0)
                {
                    foreach (var arg in frameDefinition.ResponseArguments)
                    {
                        //if (!string.IsNullOrWhiteSpace(arg.Description))
                        //{
                        //    sb.AppendLine("    /// <summary>");
                        //    sb.AppendLine($"    /// {arg.Description.XmlEscape().AddXmlCommentPrefixAfterLineBreak(1)}");
                        //    sb.AppendLine("    /// </summary>");
                        //}

                        //string csharpType = MapCTypes.MapBaseCType(arg.Type);
                        //sb.AppendLine($"    public {csharpType} {arg.Name} {{ get; set; }}");
                        //sb.AppendLine();
                    }
                }
                sb.AppendLine("\t}");

                sb.AppendLine("}");

                sb.AppendLine("}");

                logger.LogDebug("Generated response class {commandName} with {argCount} arguments",
                    className, frameDefinition.ResponseArguments?.Count ?? 0);

                return sb.ToString();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing frame definition for response {commandName}",
                    frameDefinition?.CommandName ?? "unknown");
                return string.Empty;
            }
        }

        

    }
}
