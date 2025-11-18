using YamlDotNet.Serialization;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Models
{
    /// <summary>
    /// Represents an argument in a frame's command or response
    /// </summary>
    public class FrameArgument
    {
        [YamlMember(Alias = "type")]
        public string Type { get; set; } = string.Empty;

        [YamlMember(Alias = "name")]
        public string Name { get; set; } = string.Empty;

        [YamlMember(Alias = "description")]
        public string? Description { get; set; }

        [YamlMember(Alias = "combinedArg")]
        public string? CombinedArg { get; set; }


        public bool IsArrayType { get { return Type.IndexOf('[') > -1 && Type.IndexOf(']') > -1 && Type.IndexOf('[') < Type.IndexOf(']'); } }

        public (bool isSymbolicValue, int numericValue, string symbolicValue) GetArrayLength()
        {
            int startIndex = Type.IndexOf('[');
            int endIndex = Type.IndexOf(']');
            if (startIndex >= 0 && endIndex > startIndex)
            {
                string arraySizeStr = Type.Substring(startIndex + 1, endIndex - startIndex - 1);
                bool isNumeric = int.TryParse(arraySizeStr, out int arraySize);
                if (isNumeric)
                {
                    return (false, arraySize, string.Empty);
                }
                else
                {
                    return (true, -1, arraySizeStr);
                }
            }
            return (false, 0, string.Empty);
        }

        public string GetBaseType()
        {
            if (IsArrayType)
            {
                return Type.Substring(0, Type.IndexOf('['));
            }
            else
            {
                return Type;
            }
        }
    }
}

