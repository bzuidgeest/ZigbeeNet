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
    internal static class EnumDefinitionProcessor
    {
        /// <summary>
        /// Processes an enum definition and generates C# enum code.
        /// </summary>
        /// <param name="logger">Logger instance for diagnostic output.</param>
        /// <param name="section">The section name (used for namespace generation).</param>
        /// <param name="enumDef">The enum definition to process.</param>
        /// <returns>Generated C# enum code as a string.</returns>
        internal static string ProcessEnumDefinition(ILogger<EZSPYAMLDefinitionParser> logger, string? section, EnumDefinition enumDef)
        {
            try
            {
                if (enumDef == null)
                {
                    logger.LogWarning("Enum definition is null");
                    return string.Empty;
                }

                if (string.IsNullOrWhiteSpace(enumDef.Name))
                {
                    logger.LogWarning("Enum name is empty");
                    return string.Empty;
                }

                if (Sanitize.IsReservedKeyword(enumDef.Name))
                {
                    logger.LogWarning("Enum name a reserved keyword");
                    return "// Skipping generating enums with reserved keywords as names";
                }

                var sb = new StringBuilder();

                string sanitizedEnumerationName = Sanitize.EnumerationName(enumDef.Name);

                // Generate namespace
                string sanitizedSectionName = Sanitize.SectionName(section);
                sb.AppendLine($"namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.{sanitizedSectionName}.Enumerations;");
                sb.AppendLine();

                // Generate XML documentation
                if (!string.IsNullOrWhiteSpace(enumDef.Description))
                {
                    sb.AppendLine("/// <summary>");
                    sb.AppendLine($"/// {enumDef.Description.XmlEscape()}");
                    sb.AppendLine("/// </summary>");
                }

                // Map C type to C# type
                string csharpType = MapCTypes.MapBaseCType(enumDef.Type);

                // Generate enum declaration
                sb.AppendLine($"public enum {sanitizedEnumerationName} : {csharpType}");
                sb.AppendLine("{");

                // Generate enum items
                if (enumDef.Items != null && enumDef.Items.Count > 0)
                {
                    for (int i = 0; i < enumDef.Items.Count; i++)
                    {
                        var item = enumDef.Items[i];

                        // Add XML documentation for enum item
                        if (!string.IsNullOrWhiteSpace(item.Description))
                        {
                            sb.AppendLine("    /// <summary>");
                            sb.AppendLine($"    /// {item.Description.XmlEscape()}");
                            sb.AppendLine("    /// </summary>");
                        }

                        // Add enum item with value
                        sb.Append($"    {item.Name} = {item.Value}");

                        // Add comma if not the last item
                        if (i < enumDef.Items.Count - 1)
                        {
                            sb.AppendLine(",");
                        }
                        else
                        {
                            sb.AppendLine();
                        }
                    }
                }

                sb.AppendLine("}");

                logger.LogDebug("Generated enum {enumName} with {itemCount} items", sanitizedEnumerationName, enumDef.Items?.Count ?? 0);

                return sb.ToString();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing enum definition {enumName}", enumDef?.Name ?? "unknown");
                return string.Empty;
            }
        }

    }
}
