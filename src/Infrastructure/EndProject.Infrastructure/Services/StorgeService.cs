using EndProject.Application.Abstraction.Services;
using Microsoft.AspNetCore.Http;

namespace EndProject.Infrastructure.Services;

public class StorgeService : IStorgeService
{
    private readonly IStorageFile _storageFile;
    public StorgeService(IStorageFile storageFile) => _storageFile = storageFile;
    public Task<bool> DeleteFileAsync(string pathOrContainerName, string fileName)
    => _storageFile.DeleteFileAsync(pathOrContainerName, fileName);

    public Task<byte[]> DownlandFile(string file)
    => _storageFile.DownlandFile(file);

    public Task<List<string>> GetFilesAsync(string pathOrContainerName)
    => _storageFile.GetFilesAsync(pathOrContainerName);

    public Task<bool> HasFile(string pathOrContainerName, string fileName)
    => _storageFile.HasFile(pathOrContainerName, fileName);

    public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
    => _storageFile.UploadAsync(pathOrContainerName, files);

    public Task<string> UploadFileAsync(string pathOrContainerName, byte[] fileData, string fileName)
    => _storageFile.UploadFileAsync(pathOrContainerName, fileData, fileName);

    public Task<string> WriteFile(string pathOrContainerName, IFormFile file)
    => _storageFile.WriteFile(pathOrContainerName, file);
}

