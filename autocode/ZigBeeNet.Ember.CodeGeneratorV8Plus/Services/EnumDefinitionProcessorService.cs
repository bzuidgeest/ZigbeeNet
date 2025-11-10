using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Models;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Models.TypeMapper;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Parser;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Utility;
using static System.Collections.Specialized.BitVector32;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Services
{
    internal class EnumDefinitionProcessorService
    {
        private readonly ILogger<EnumDefinitionProcessorService> _logger;
        private readonly TypeMapperService _typeMapperService;
        private readonly FileService _fileService;

        public EnumDefinitionProcessorService(ILoggerFactory _loggerFactory, TypeMapperService typeMapperService, FileService fileService)
        {
            _logger = _loggerFactory.CreateLogger<EnumDefinitionProcessorService>();
            _typeMapperService = typeMapperService;
            _fileService = fileService;
        }

        /// <summary>
        /// Processes an enum definition and generates C# enum code.
        /// </summary>
        /// <param name="_logger">Logger instance for diagnostic output.</param>
        /// <param name="sectionName">The sectionName name (used for namespace generation).</param>
        /// <param name="enumDefinition">The enum definition to process.</param>
        /// <returns>Generated C# enum code as a string.</returns>
        internal bool Process(string sectionName, EnumDefinition enumDefinition)
        {
            if (enumDefinition == null)
            {
                _logger.LogWarning("Enum definition is null");
                return false;
            }

            if (string.IsNullOrWhiteSpace(enumDefinition.Name))
            {
                _logger.LogWarning("Enum name is empty");
                return false;
            }

            if (Sanitize.IsReservedKeyword(enumDefinition.Name))
            {
                _logger.LogWarning("Enum name a reserved keyword, skipping");
                return true;
            }

            // Map C type to C# type
            TypeMapping? enumBaseTypeMapping = _typeMapperService.GetTypeMapping(enumDefinition.Type);

            if (enumBaseTypeMapping == null)
            {
                _logger.LogError("No type mapping found for base type {type} of enum {enumName} in section {section}, cannot finish",
                    enumDefinition.Type, enumDefinition.Name, sectionName);
                return false;
            }

            
            _typeMapperService.AddTypeMapping(
                new CType(enumDefinition.Name, enumBaseTypeMapping.Value.CType.SizeInBytes, enumDefinition.Description)
                {
                    IsEnum = true,
                    UnderlyingTypeName = enumBaseTypeMapping.Value.CType.Name
                },
                new CSharpType(Sanitize.EnumerationName(enumDefinition.Name))
                {
                    IsEnum = true,
                    UnderlyingTypeName = enumBaseTypeMapping.Value.CSharpType.Name,
                }
            );

            var sb = new StringBuilder();

            string sanitizedEnumerationName = Sanitize.EnumerationName(enumDefinition.Name);

            // Generate namespace
            sb.AppendLine($"namespace ZigBeeNet.Hardware.EmberV8Plus.Ezsp.{sectionName}.Enumerations;");
            sb.AppendLine();

            // Generate XML documentation
            if (!string.IsNullOrWhiteSpace(enumDefinition.Description))
            {
                TextHelper.AppendBasicSummary(sb, enumDefinition.Description);
            }

            // Generate enum declaration
            sb.AppendLine($"public enum {sanitizedEnumerationName} : {enumBaseTypeMapping?.CSharpType.Name}");
            sb.AppendLine("{");

            // Generate enum items
            if (enumDefinition.Items != null && enumDefinition.Items.Count > 0)
            {
                for (int i = 0; i < enumDefinition.Items.Count; i++)
                {
                    var item = enumDefinition.Items[i];

                    // Add XML documentation for enum item
                    if (!string.IsNullOrWhiteSpace(item.Description))
                    {
                        TextHelper.AppendBasicSummary(sb, item.Description, 1);
                    }

                    // Add enum item with value
                    if (String.IsNullOrEmpty(item.Value))
                        sb.Append($"\t\t{item.Name}");
                    else
                        sb.Append($"\t\t{item.Name} = {item.Value}");

                    // Add comma if not the last item
                    if (i < enumDefinition.Items.Count - 1)
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

            _logger.LogDebug("Generated enum {enumName} with {itemCount} items", sanitizedEnumerationName, enumDefinition.Items?.Count ?? 0);

            _fileService.SaveEnumFile(sectionName, sanitizedEnumerationName, sb.ToString());

            return true;
        }
    }
}
