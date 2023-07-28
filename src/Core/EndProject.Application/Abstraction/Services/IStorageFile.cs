using Microsoft.AspNetCore.Http;

namespace EndProject.Application.Abstraction.Services;

public interface IStorageFile
{
    Task<string> WriteFile(string pathOrContainerName, IFormFile file);
    //--
    Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files);
    Task<string> UploadFileAsync(string pathOrContainerName, byte[] fileData, string fileName);
    //--
    Task<byte[]> DownlandFile(string file);
    Task<bool> DeleteFileAsync(string pathOrContainerName, string fileName);
    Task<List<string>> GetFilesAsync(string pathOrContainerName);
    Task<bool> HasFile(string pathOrContainerName, string fileName);
}
