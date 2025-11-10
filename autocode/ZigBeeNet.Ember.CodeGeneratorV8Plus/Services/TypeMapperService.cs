using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Models.TypeMapper;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Services
{
    internal class TypeMapperService
    {
        private readonly ILogger _logger;

        private readonly Dictionary<string, TypeMapping> _typeMappings = new();

        public TypeMapperService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TypeMapperService>();

            // Initialize with basic type mappings
            AddTypeMapping(new CType("uint8_t", 1, ""), new CSharpType("byte"));
            AddTypeMapping(new CType("int8_t", 1, ""), new CSharpType("sbyte"));
            AddTypeMapping(new CType("uint16_t", 2, ""), new CSharpType("ushort"));
            AddTypeMapping(new CType("int16_t", 2, ""), new CSharpType("short"));
            AddTypeMapping(new CType("uint32_t", 4, ""), new CSharpType("uint"));
            AddTypeMapping(new CType("int32_t", 4, ""), new CSharpType("int"));
            AddTypeMapping(new CType("uint64_t", 8, ""), new CSharpType("ulong"));
            AddTypeMapping(new CType("int64_t", 8, ""), new CSharpType("long"));
            AddTypeMapping(new CType("bool", 1, ""), new CSharpType("bool"));
        }


        public void AddTypeMapping(CType cType, CSharpType cSharpType)
        {
            if (_typeMappings.TryAdd(cType.Name, new TypeMapping(cType, cSharpType)) == true)

            {
                _logger.LogInformation($"Added type mapping: C Type '{cType.Name}' -> C# Type '{cSharpType.Name}'");
            }
            else
            {
                _logger.LogWarning($"Type mapping for C Type '{cType.Name}' already exists. Skipping addition.");
            }
        }

        public TypeMapping? GetTypeMapping(string cTypeName)
        {
            if (_typeMappings.TryGetValue(cTypeName, out var mapping))
            {
                return mapping;
            }
            else
            {
                _logger.LogWarning($"Type mapping for C Type '{cTypeName}' not found.");
                return null;
            }
        }
        public bool SectionTypeMappingExists(string sectionName)
        {
            return _typeMappings.Any(tm => tm.Value.CSharpType.Namespace.Contains(sectionName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
