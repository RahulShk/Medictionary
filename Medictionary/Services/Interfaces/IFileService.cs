namespace Medictionary.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(string dirPath,string fileName, IFormFile formFile);
    }
}