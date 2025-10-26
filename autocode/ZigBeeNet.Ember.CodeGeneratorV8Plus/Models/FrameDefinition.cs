using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Models
{
    /// <summary>
    /// Represents a frame definition in the EZSP protocol
    /// </summary>
    public class FrameDefinition
    {
        [YamlMember(Alias = "value")]
        public string Value { get; set; }

        [YamlMember(Alias = "needGeneratedHandler")]
        public string NeedGeneratedHandler { get; set; }

        [YamlMember(Alias = "commandName")]
        public string CommandName { get; set; }

        [YamlMember(Alias = "description")]
        public string Description { get; set; }

        [YamlMember(Alias = "commandArguments")]
        public List<FrameArgument> CommandArguments { get; set; } = new();

        [YamlMember(Alias = "responseArguments")]
        public List<FrameArgument> ResponseArguments { get; set; } = new();
    }
}

