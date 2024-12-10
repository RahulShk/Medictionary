
using Medictionary.Services.Interfaces;

namespace Medictionary.Services
{

    public class FileService(IWebHostEnvironment environment) : IFileService
    { 
        private readonly IWebHostEnvironment _environment = environment;

        public async Task<string> SaveFileAsync(string dirPath,string fileName, IFormFile formFile)
        {
            var fullPath = $"{_environment.WebRootPath}/{dirPath}";
            if (!Directory.Exists(fullPath))
                Directory.CreateDirectory(fullPath);
            var filePath = $"{fullPath}/{fileName}";

            using var stream = File.Create(filePath);
            await formFile.CopyToAsync(stream);
            return $"{dirPath}/{fileName}";
        }
    }
}

