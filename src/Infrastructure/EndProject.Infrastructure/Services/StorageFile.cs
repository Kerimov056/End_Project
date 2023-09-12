using EndProject.Application.Abstraction.Services.Stroge;
using Microsoft.AspNetCore.Http;

namespace EndProject.Infrastructure.Services;

public class StorageFile : IStorageFile
{
    public async Task<bool> DeleteFileAsync(string pathOrContainerName, string fileName)
    {
        string filePath = Path.Combine(pathOrContainerName, fileName);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<byte[]> DownlandFile(string file)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", file);  //Hara endirirsen oranida qoyarssan
        return await System.IO.File.ReadAllBytesAsync(filePath);
    }

    public async Task<List<string>> GetFilesAsync(string pathOrContainerName)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(pathOrContainerName);
        if (!directoryInfo.Exists)
        {
            return new List<string>();
        }

        List<string> fileNames = directoryInfo.GetFiles().Select(file => file.Name).ToList();
        return fileNames;
    }

    public async Task<bool> HasFile(string pathOrContainerName, string fileName)
    {
        string filePath = Path.Combine(pathOrContainerName, fileName);
        if (File.Exists(filePath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
    {
        List<(string fileName, string pathOrContainerName)> uploadFiles = new List<(string fileName, string pathOrContainerName)>();

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                byte[] fileData;
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    fileData = stream.ToArray();
                }

                string uploadedFileName = file.FileName;
                string uploadedFilePath = await UploadFileAsync(pathOrContainerName, fileData, uploadedFileName);

                uploadFiles.Add((uploadedFileName, uploadedFilePath));

            }
        }
        return uploadFiles;
    }

    public async Task<string> UploadFileAsync(string pathOrContainerName, byte[] fileData, string fileName)
    {
        string filePath = Path.Combine(pathOrContainerName, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.WriteAsync(fileData, 0, fileData.Length);
        }

        return filePath;
    }

    public async Task<string> WriteFile(string pathOrContainerName, IFormFile file)
    {
        string filename = "";
        var extension = "." + file.FileName.Split(".")[file.FileName.Split('.').Length - 1];
        filename = DateTime.Now.Ticks.ToString() + extension;

        var filepath = Path.Combine(Directory.GetCurrentDirectory(), pathOrContainerName);

        if (!Directory.Exists(filepath))
        {
            Directory.CreateDirectory(filepath);
        }

        var exactpath = Path.Combine(Directory.GetCurrentDirectory(), pathOrContainerName, filename);
        using (var stream = new FileStream(exactpath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return filename;
    }


    public string ConvertFileToBase64(string pathOrContainerName, IFormFile file)
    {
        using (var memoryStream = new MemoryStream())
        {
            file.CopyTo(memoryStream);
            byte[] bytes = memoryStream.ToArray();
            return Convert.ToBase64String(bytes);
        }
    }

}
