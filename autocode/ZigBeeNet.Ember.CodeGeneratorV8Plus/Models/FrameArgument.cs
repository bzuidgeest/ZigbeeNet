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
    }
}

