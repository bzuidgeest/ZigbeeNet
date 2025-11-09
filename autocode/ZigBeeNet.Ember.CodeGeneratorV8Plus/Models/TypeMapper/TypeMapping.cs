using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Models.TypeMapper
{
    internal record struct TypeMapping
    {
        public CType CType { get; init; }
        public CSharpType CSharpType { get; init; }
        public TypeMapping(CType cType, CSharpType cSharpType)
        {
            CType = cType;
            CSharpType = cSharpType;
        }
    }
}
