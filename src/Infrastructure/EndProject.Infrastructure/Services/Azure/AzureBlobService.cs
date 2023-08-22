using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace EndProject.Infrastructure.Services.Azure;

public class AzureBlobService
{
    BlobServiceClient _BlobServiceClient;
    BlobContainerClient _BlobContainerClient;
    string azureConnectString="key";

    public AzureBlobService()
    {
        _BlobServiceClient = new BlobServiceClient(azureConnectString);
        _BlobContainerClient = _BlobServiceClient.GetBlobContainerClient("Contiener Name");
    }

    public async Task<List<BlobContentInfo>> UploadFiles(List<IFormFile> files)
    {
        var azureResponse = new List<BlobContentInfo>();
        foreach (var file in files)
        {
            string fileName = file.Name;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;

                var client = await _BlobContainerClient.UploadBlobAsync(fileName, memoryStream, default);
                azureResponse.Add(client);
            }
        }
        return azureResponse;
    }


    public async Task<List<BlobItem>> GetUploadedBlob()
    {
        var items =new List<BlobItem>();
        var UploadFiles = _BlobContainerClient.GetBlobsAsync();
        await foreach (BlobItem file in UploadFiles)
        {
            items.Add(file);
        }
        return items;
    }
}
