using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Models.TypeMapper
{
    internal class CSharpType
    {
        public string Name { get; set; } = string.Empty;
        public bool IsValueType { get; set; } = false;
        public bool IsNullable { get; set; } = false;
        public bool IsArray { get; set; } = false;
        public int ArrayLength { get; set; } = 0;
        public int StringLength { get; set; } = 0;

        //public int TypeByteSize { get; set; } = 0;

        public string UnderlyingTypeName { get; set; } = string.Empty;

        public bool IsEnum { get; set; } = false;
        public bool IsString { get; set; } = false;
        public bool IsBoolean { get; set; } = false;
        public bool IsStruct { get; set; } = false;

        public CSharpType(string name) 
        { 
            Name = name;
        }


    }
}
