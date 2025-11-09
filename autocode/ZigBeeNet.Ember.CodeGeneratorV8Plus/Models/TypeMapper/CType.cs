using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Models.TypeMapper
{
    internal class CType
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
        public int SizeInBytes { get; set; } = 0;

        public bool IsEnum { get; set; } = false;
        public string UnderlyingTypeName { get; set; } = string.Empty;

        public CType(string name, int sizeInBytes, string description)
        {
            Name = name;
            SizeInBytes = sizeInBytes;
            Description = description;
        }
    }
}
