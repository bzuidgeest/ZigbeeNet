namespace ZigBeeNet.EmberV8Plus.CodeGenerator
{
    /// <summary>
    /// Application configuration settings loaded from appsettings.json
    /// </summary>
    public class ApplicationSettings
    {
        /// <summary>
        /// Gets or sets the output directory where generated code files will be written.
        /// </summary>
        public string OutputDirectory { get; set; } = "bin/Debug/Generated";

        public string ParseVersion { get; set; } = "2025.6.2";
    }
}

