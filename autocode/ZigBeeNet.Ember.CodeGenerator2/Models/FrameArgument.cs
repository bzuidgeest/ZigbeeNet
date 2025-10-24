using YamlDotNet.Serialization;

namespace ZigBeeNet.Ember.CodeGenerator2.Models
{
    /// <summary>
    /// Represents an argument in a frame's command or response
    /// </summary>
    public class FrameArgument
    {
        [YamlMember(Alias = "type")]
        public string? Type { get; set; }

        [YamlMember(Alias = "name")]
        public string? Name { get; set; }

        [YamlMember(Alias = "description")]
        public string? Description { get; set; }

        [YamlMember(Alias = "combinedArg")]
        public string? CombinedArg { get; set; }
    }
}

