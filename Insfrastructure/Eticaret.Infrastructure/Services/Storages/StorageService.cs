namespace EticaretAPI.Infrastructure.Services.Storages;
using EticaretAPI.Application.Storage;
using Microsoft.AspNetCore.Http; 


public class StorageService : IStorageService
{
    readonly IStorage _storage;

    public StorageService(IStorage storage)
    {
        _storage = storage;
    }

    public string StorageName { get => _storage.GetType().Name;  }

    public void Delete(string pathOrContainerName , string fileName) => _storage.Delete(pathOrContainerName , fileName);

    public List<string> GetFiles(string pathOrContainerName)
    => _storage.GetFiles(pathOrContainerName);

    public bool HasFile(string pathOrContainerName, string fileName)
    => _storage.HasFile(pathOrContainerName, fileName);

    public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, IFormFileCollection files)
    => _storage.UploadAsync(pathOrContainerName, files);
}
