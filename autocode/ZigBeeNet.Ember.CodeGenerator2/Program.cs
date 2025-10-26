/*
PSEUDOCODE / PLAN (detailed):
1. Use Generic Host to add dependency injection and default logging.
2. Configure console logging with a simple format and Information level as default.
3. Register a background worker that will run the existing processing logic.
   - Create a BackgroundService-derived class `Worker` that runs on start.
   - Inject ILogger and IServiceProvider into `Worker`.
4. Move the existing folder scanning & processing logic into a service class `VersionProcessor`.
   - `VersionProcessor` will be resolved via DI (ActivatorUtilities) inside the worker.
   - Keep semantics: log warnings if no resources or missing ezsp.yaml, log info for found versions.
5. Main builds and runs the host; the Worker runs the processing then exits.
6. Preserve exception handling and make logs informative.
7. Keep everything in this single file for minimal change and easy integration.

Behavior:
- On application start, host and default logging are configured.
- `Worker` executes `VersionProcessor.ProcessAsync`.
- Processing logs are emitted via ILogger.
- Application exits after work completes (BackgroundService finishes).
*/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using ZigBeeNet.Ember.CodeGenerator2.Models;

namespace ZigBeeNet.Ember.CodeGenerator2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddSimpleConsole(options =>
                    {
                        options.SingleLine = true;
                        options.TimestampFormat = "HH:mm:ss ";
                    });
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddTransient<VersionProcessor>();
                })
                .Build();

            await host.RunAsync();
        }
    }

    internal class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _provider;
        private readonly IHostApplicationLifetime _lifetime;

        public Worker(ILogger<Worker> logger, IServiceProvider provider, IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _provider = provider;
            _lifetime = lifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Worker starting processing...");
                // Resolve the processor from DI and run it
                var processor = ActivatorUtilities.CreateInstance<VersionProcessor>(_provider);
                await processor.ProcessAsync(stoppingToken);
                _logger.LogInformation("Worker finished processing.");
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Processing cancelled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception during processing.");
            }
            finally
            {
                // Signal the host to stop after work completes
                _lifetime.StopApplication();
            }
        }
    }

    internal class VersionProcessor
    {
        private readonly ILogger<VersionProcessor> _logger;

        public VersionProcessor(ILogger<VersionProcessor> logger)
        {
            _logger = logger;
        }

        public Task ProcessAsync(CancellationToken cancellationToken)
        {
            try
            {
                // Find all version folders in Resources directory
                // Look for Resources relative to the project directory
                string projectDir = Directory.GetCurrentDirectory();
                string resourcesPath = Path.Combine(projectDir, "Resources", "sisdk");

                if (!Directory.Exists(resourcesPath))
                {
                    _logger.LogWarning("Resources path not found: {path}", resourcesPath);
                    return Task.CompletedTask;
                }

                var versionDirs = Directory.GetDirectories(resourcesPath).ToList();

                if (versionDirs.Count == 0)
                {
                    _logger.LogWarning("No version folders found in Resources directory");
                    return Task.CompletedTask;
                }

                _logger.LogInformation("Found {count} version folder(s)", versionDirs.Count);

                foreach (var versionDir in versionDirs)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    string versionName = Path.GetFileName(versionDir);
                    string definitionPath = Path.Combine(versionDir, "ezsp.yaml");

                    if (!File.Exists(definitionPath))
                    {
                        _logger.LogWarning("{path} not found, skipping {version}", definitionPath, versionName);
                        continue;
                    }

                    _logger.LogInformation("KDProcessing version {version} at {path}", versionName, definitionPath);
                    ProcessVersion(versionDir, definitionPath, versionName);
                }

                _logger.LogInformation("Code generation processing completed successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing versions.");
            }

            return Task.CompletedTask;
        }

        public void ProcessVersion(string versionDir, string definitionPath, string versionName)
        {
            try
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .WithTypeConverter(new TypedefDefinitionConverter())
                    .Build();

                using (TextReader definitionFileReader = new StreamReader(definitionPath))
                {
                    var sections = deserializer.Deserialize<List<EzspSection>>(definitionFileReader);

                    if (sections == null || sections.Count == 0)
                    {
                        _logger.LogWarning("No sections found in {file}", definitionPath);
                        return;
                    }

                    _logger.LogInformation("Successfully deserialized {count} section(s) from {version}", sections.Count, versionName);

                    foreach (var section in sections)
                    {
                        _logger.LogInformation("  Section: {section}", section.Section);

                        if (section.Typedefs != null && section.Typedefs.Count > 0)
                        {
                            var simpleTypedefs = new List<Typedef>();
                            var simpleCount = 0;
                            var complexCount = 0;

                            foreach (var typedef in section.Typedefs)
                            {
                                if (typedef.Definition is SimpleTypedefDefinition simple)
                                {
                                    simpleCount++;
                                    simpleTypedefs.Add(typedef);
                                    _logger.LogInformation("      - {name} (simple): {type}", typedef.Name, simple.Type);
                                }
                                else if (typedef.Definition is ComplexTypedefDefinition complex)
                                {
                                    complexCount++;
                                    _logger.LogInformation("      - {name} (complex): {fieldCount} fields", typedef.Name, complex.Fields.Count);
                                    ProcessComplexTypeDefinition(section.Section, typedef);
                                }
                            }

                            // Process all simple typedefs for this section at once
                            if (simpleTypedefs.Count > 0)
                            {
                                ProcessSimpleTypeDefinitions(section.Section, simpleTypedefs);
                            }

                            _logger.LogInformation("    Found {count} typedef(s) - {simple} simple, {complex} complex",
                                section.Typedefs.Count, simpleCount, complexCount);
                        }

                        if (section.Enums != null && section.Enums.Count > 0)
                        {
                            _logger.LogInformation("    Found {count} enum(s)", section.Enums.Count);
                            foreach (var enumDef in section.Enums)
                            {
                                _logger.LogInformation("      - {name} ({type}): {itemCount} items", enumDef.Name, enumDef.Type, enumDef.Items?.Count ?? 0);
                            }
                        }

                        if (section.Frames != null && section.Frames.Count > 0)
                        {
                            _logger.LogInformation("    Found {count} frame(s)", section.Frames.Count);
                            foreach (var frame in section.Frames)
                            {
                                _logger.LogInformation("      - {name} ({value}): {cmdArgs} cmd args, {respArgs} resp args",
                                    frame.CommandName, frame.Value,
                                    frame.CommandArguments?.Count ?? 0,
                                    frame.ResponseArguments?.Count ?? 0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing version {version}", versionName);
            }
        }

        private void ProcessSimpleTypeDefinitions(string sectionName, List<Typedef> typedefs)
        {
            try
            {
                if (typedefs == null || typedefs.Count == 0)
                {
                    return;
                }

                // Create output directory for this section
                string projectDir = Directory.GetCurrentDirectory();
                string outputDir = Path.Combine(projectDir, "bin", "Debug", "net9.0", "Generated", SanitizeSectionName(sectionName));
                Directory.CreateDirectory(outputDir);

                // Create a file for type aliases
                string fileName = "TypeAliases.cs";
                string filePath = Path.Combine(outputDir, fileName);

                // Generate the complete file with all typedefs
                var content = GenerateTypeAliasFile(sectionName, typedefs);
                File.WriteAllText(filePath, content);
                _logger.LogDebug("Created type alias file with {count} aliases: {path}", typedefs.Count, filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing simple typedefs for section {section}", sectionName);
            }
        }

        private static string GenerateTypeAliasFile(string sectionName, List<Typedef> typedefs)
        {
            var sb = new System.Text.StringBuilder();

            // No namespace declaration - global using directives must be at file level

            foreach (var typedef in typedefs)
            {
                if (typedef.Definition is SimpleTypedefDefinition simpleTypedefDefinition)
                {
                    if (!string.IsNullOrEmpty(typedef.Description))
                    {
                        sb.AppendLine($"/// <summary>");
                        sb.AppendLine($"/// {typedef.Description}");
                        sb.AppendLine($"/// </summary>");
                    }

                    // Check if this is an array type
                    int bracketIndex = simpleTypedefDefinition.Type.IndexOf('[');
                    if (bracketIndex >= 0)
                    {
                        if (typedef.Name.Contains("string"))
                        {
                            // (array types cannot be used in type aliases, see if they work as string....)");
                            sb.AppendLine($"/// <remarks>Original C type: {simpleTypedefDefinition.Type}</remarks>");
                            sb.AppendLine($"global using {typedef.Name} = string;");
                        }
                        else
                        {
                            // Array type - cannot use in alias, skip it
                            sb.AppendLine($"/// <remarks>Original C type: {simpleTypedefDefinition.Type} (array types cannot be used in type aliases)</remarks>");
                            sb.AppendLine($"// Skipped: {typedef.Name}");
                        }
                    }
                    else
                    {
                        // Simple scalar type - use global using alias
                        string csharpType = MapBaseCType(simpleTypedefDefinition.Type);
                        sb.AppendLine($"/// <remarks>Original C type: {simpleTypedefDefinition.Type}</remarks>");
                        sb.AppendLine($"global using {typedef.Name} = {csharpType};");
                    }
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private static string GenerateComplexTypeDefinition(Typedef typedef, ComplexTypedefDefinition complex)
        {
            var sb = new System.Text.StringBuilder();

            // Add XML documentation
            if (!string.IsNullOrEmpty(typedef.Description))
            {
                sb.AppendLine("/// <summary>");
                sb.AppendLine($"/// {typedef.Description}");
                sb.AppendLine("/// </summary>");
            }

            // Generate struct definition
            sb.AppendLine($"public struct {typedef.Name}");
            sb.AppendLine("{");

            // Add fields
            foreach (var field in complex.Fields)
            {
                if (!string.IsNullOrEmpty(field.Description))
                {
                    sb.AppendLine("    /// <summary>");
                    sb.AppendLine($"    /// {field.Description}");
                    sb.AppendLine("    /// </summary>");
                }

                // Check if the field name contains array notation (e.g., fieldName[SIZE])
                int nameBracketIndex = field.Name?.IndexOf('[') ?? -1;
                if (nameBracketIndex >= 0)
                {
                    // Extract field name and array size from name
                    string fieldName = field.Name![..nameBracketIndex];
                    string arrayPart = field.Name[nameBracketIndex..];
                    string arraySize = arrayPart.Trim('[', ']');
                    string csharpBaseType = MapBaseCType(field.Type);

                    // Check if array size is numeric (compile-time constant)
                    if (int.TryParse(arraySize, out _))
                    {
                        // Use fixed keyword for fixed-size arrays with numeric constants
                        sb.AppendLine($"    public fixed {csharpBaseType} {fieldName}[{arraySize}];");
                    }
                    else
                    {
                        // Array size is a symbolic constant - use a comment and skip
                        sb.AppendLine($"    // Array field with symbolic size: {arraySize}");
                        sb.AppendLine($"    // public fixed {csharpBaseType} {fieldName}[{arraySize}];");
                    }
                }
                // Check if the type contains array notation (e.g., uint8_t[8])
                else if (field.Type?.IndexOf('[') >= 0)
                {
                    int typeBracketIndex = field.Type.IndexOf('[');
                    // Extract base type and array size
                    string baseType = field.Type[..typeBracketIndex];
                    string arrayPart = field.Type[typeBracketIndex..];
                    string csharpBaseType = MapBaseCType(baseType);

                    // Extract array size from [N] format
                    string arraySize = arrayPart.Trim('[', ']');

                    // Check if array size is numeric (compile-time constant)
                    if (int.TryParse(arraySize, out _))
                    {
                        // Use fixed keyword for fixed-size arrays with numeric constants
                        sb.AppendLine($"    public fixed {csharpBaseType} {field.Name}[{arraySize}];");
                    }
                    else
                    {
                        // Array size is a symbolic constant - use a comment and skip
                        sb.AppendLine($"    // Array field with symbolic size: {arraySize}");
                        sb.AppendLine($"    // public fixed {csharpBaseType} {field.Name}[{arraySize}];");
                    }
                }
                else
                {
                    // Simple scalar type
                    string csharpType = MapBaseCType(field.Type);
                    sb.AppendLine($"    public {csharpType} {field.Name};");
                }
                sb.AppendLine();
            }

            sb.AppendLine("}");
            sb.AppendLine();

            return sb.ToString();
        }

        /// <summary>
        /// Maps base C types to C# types.
        /// </summary>
        private static string MapBaseCType(string cType)
        {
            return cType.Trim() switch
            {
                "uint8_t" => "byte",
                "int8_t" => "sbyte",
                "uint16_t" => "ushort",
                "int16_t" => "short",
                "uint32_t" => "uint",
                "int32_t" => "int",
                "uint64_t" => "ulong",
                "int64_t" => "long",
                "bool" => "bool",
                _ => cType // Return original if no mapping found
            };
        }

        private static string SanitizeSectionName(string sectionName)
        {
            // Remove spaces and special characters, convert to PascalCase
            return System.Text.RegularExpressions.Regex.Replace(sectionName, @"[^a-zA-Z0-9]", "");
        }

        private void ProcessComplexTypeDefinition(string sectionName, Typedef typedef)
        {
            try
            {
                if (typedef?.Definition is not ComplexTypedefDefinition complex)
                {
                    return;
                }

                // Create output directory for this section
                string projectDir = Directory.GetCurrentDirectory();
                string sanitizedSectionName = SanitizeSectionName(sectionName);
                string outputDir = Path.Combine(projectDir, "bin", "Debug", "net9.0", "Generated", sanitizedSectionName);
                Directory.CreateDirectory(outputDir);

                // Create a separate file for each struct
                string fileName = $"{typedef.Name}.cs";
                string filePath = Path.Combine(outputDir, fileName);

                // Generate the struct definition
                var content = GenerateComplexTypeDefinition(typedef, complex);

                // Create file with namespace wrapper using section name
                var fileContent = new System.Text.StringBuilder();
                fileContent.AppendLine($"namespace ZigBeeNet.Ember.Generated.{sanitizedSectionName};");
                fileContent.AppendLine();
                fileContent.Append(content);
                File.WriteAllText(filePath, fileContent.ToString());

                _logger.LogDebug("Created complex type definition: {path}", filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing complex typedef {name} for section {section}", typedef?.Name, sectionName);
            }
        }
    }
}