using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Parser;
using ZigBeeNet.EmberV8Plus.CodeGenerator.Utility;

namespace ZigBeeNet.EmberV8Plus.CodeGenerator.Services
{
    internal class FileService
    {
        private readonly ILogger<FileService> _logger;
        private readonly TypeMapperService _typeMapper;
        private readonly IOptions<ApplicationSettings> _appSettings;


        private readonly string _resourcesPath;
        public FileService(ILoggerFactory _loggerFactory, TypeMapperService typeMapperService, IOptions<ApplicationSettings> appSettings)
        {
            _logger = _loggerFactory.CreateLogger<FileService>();
            _typeMapper = typeMapperService;
            _appSettings = appSettings;


            _resourcesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "sisdk", appSettings.Value.ParseVersion);
        }


        public bool FileExistsInVersion(string fileName)
        {
            if (File.Exists(Path.Combine(_resourcesPath, fileName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetFile(string fileName)
        {
            return File.ReadAllText(Path.Combine(_resourcesPath, fileName));
        }

        public void SaveComplexTypeFile(string sectionName, string typeName, string content)
        {
            SaveFile(sectionName, "Types", typeName, content);
        }

        public void SaveEnumFile(string sectionName, string enumName, string content)
        {
            SaveFile(sectionName, "Enumerations", enumName, content);
        }

        public void SaveFrameFile(string sectionName, string frameName, string content)
        {
            SaveFile(sectionName, "Frames", frameName, content);
        }

        private void SaveFile(string sectionName, string subSection, string fileName, string fileContent)
        {
            string sanitizedSectionName = Sanitize.SectionName(sectionName);
            string outputPath = Path.Combine(_appSettings.Value.OutputDirectory, _appSettings.Value.ParseVersion, sanitizedSectionName, subSection);
            Directory.CreateDirectory(outputPath);

            File.WriteAllText(Path.Combine(outputPath, $"{fileName}.cs"), $"#if VERSION_{_appSettings.Value.ParseVersion.Replace('.', '_')}\r\n{fileContent}\r\n#endif");
        }
    }
}