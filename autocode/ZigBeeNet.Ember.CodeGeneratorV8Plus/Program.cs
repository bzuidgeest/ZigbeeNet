using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Parser;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Services;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Utility;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator
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
                    // Load configuration from appsettings.json
                    var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .Build();

                    // Register ApplicationSettings
                    services.Configure<ApplicationSettings>(configuration);
                    services.AddSingleton(sp => sp.GetRequiredService<IOptions<ApplicationSettings>>().Value);

                    services.AddTransient<FrameDefinitionGenerator>();

                    services.AddSingleton<TypeMapperService>();
                    services.AddSingleton<CSharpLanguageService>();
                    services.AddTransient<FileService>();
                    services.AddTransient<EnumDefinitionProcessorService>();
                    services.AddTransient<SimpleTypeDefinitionProcessorService>();
                    services.AddTransient<ComplexTypeDefinitionProcessorService>();
                    services.AddTransient<FrameDefinitionProcessorService>();
                    services.AddHostedService<Worker>();
                    services.AddTransient<EZSPDefinitionsProcessor>();
                    services.AddTransient<EZSPYAMLDefinitionParser>();
                })
                .Build();

            await host.RunAsync();
            Console.WriteLine("Application has exited. Press any key to exit this window.");
            Console.ReadKey();
        }
    }

    internal class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _provider;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly ApplicationSettings _applicationSettings;

        public Worker(ILogger<Worker> logger, IServiceProvider provider, IHostApplicationLifetime lifetime, ApplicationSettings applicationSettings)
        {
            _logger = logger;
            _provider = provider;
            _lifetime = lifetime;
            _applicationSettings = applicationSettings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Worker starting processing...");
                // Resolve the processor from DI and run it
                var processor = ActivatorUtilities.CreateInstance<EZSPDefinitionsProcessor>(_provider);
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

    internal class EZSPDefinitionsProcessor
    {
        private readonly ILogger<EZSPDefinitionsProcessor> _logger;
        private readonly EZSPYAMLDefinitionParser _eZSPYAMLDefinitionParser;
        private readonly ApplicationSettings _applicationSettings;

        public EZSPDefinitionsProcessor(ILogger<EZSPDefinitionsProcessor> logger, EZSPYAMLDefinitionParser eZSPYAMLDefinitionParser, ApplicationSettings applicationSettings)
        {
            _logger = logger;
            _eZSPYAMLDefinitionParser = eZSPYAMLDefinitionParser;
            _applicationSettings = applicationSettings;
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

                    _logger.LogInformation("Processing version {version} at {path}", versionName, definitionPath);

                    // Clear output directory before processing each version
                    //new DirectoryInfo(_applicationSettings.OutputDirectory).ClearAsync();
                    _eZSPYAMLDefinitionParser.Process(versionDir, definitionPath, versionName);
                }

                _logger.LogInformation("Code generation processing completed successfully!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing versions.");
            }

            return Task.CompletedTask;
        }

        
    }
}