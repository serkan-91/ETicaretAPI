using Azure.Storage.Blobs;
using EticaretAPI.Infrastructure.AppConfigurations.@base;
using EticaretAPI.Application.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs.Models;

namespace EticaretAPI.Infrastructure.Services.Storages.Azure;

public class AzureStorage : Storage, IAzureStorage
{
    private readonly AppConfiguration _appConfig;
      readonly BlobServiceClient _blobServiceClient;
      BlobContainerClient _blobContainerClient;


    public AzureStorage(IConfiguration configuration )
    {
        _appConfig = new AppConfiguration(configuration);

        var azureConnectionString = _appConfig.AzureStorageConfiguration?.Azure?.ConnectionString;
        _blobServiceClient = new BlobServiceClient(azureConnectionString); 
    }

    public async void Delete(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
       BlobClient blobClient=  _blobContainerClient.GetBlobClient(fileName);
        await blobClient.DeleteAsync();
    }

    public List<string> GetFiles(string containerName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
    }

    public new bool HasFile(string containerName, string fileName)
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        return _blobContainerClient.GetBlobs().Any(b => b.Name == "containerName"); 

    }

    public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
    {
          _blobContainerClient =   _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient.CreateIfNotExistsAsync();
        await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        List<(string fileName, string pathOrContainerName)> datas = new();
        foreach (var file in files)
        {
         string fileFormattedName=   await FileNameFormatConverterAsync(containerName, file.Name, HasFile);
           BlobClient blobClient = _blobContainerClient.GetBlobClient(fileFormattedName);
            await blobClient.UploadAsync(file.OpenReadStream());

        }
        return datas;
    }
}
