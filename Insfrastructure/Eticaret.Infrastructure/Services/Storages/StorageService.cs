using EticaretAPI.Application.Abstractions.Storage;
using EticaretAPI.Application.Common.Dtos;
using EticaretAPI.Application.ResponseParameters;
using static EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.Infrastructure.Services.Storage;

public class StorageService(IStorage _storage, StorageType StorageType) : IStorageService
{
    public StorageType StorageServiceType => StorageType;

    public string GetBasePathOrContainer => _storage.GetBasePathOrContainer;

    public Task<bool> DeleteFileAsync(
        string pathOrContainer,
        string fileName,
        CancellationToken cancellationToken
    ) =>
        Task.Run(
            () => (_storage.DeleteFileAsync(pathOrContainer, fileName, cancellationToken)),
            cancellationToken
        );

    public string GetBasePath() => string.Empty;

    public async Task<List<string>> GetFilesAsync(
        string pathOrContainer,
        CancellationToken cancellationToken
    ) => await _storage.GetFilesAsync(pathOrContainer, cancellationToken).ConfigureAwait(false);

    public Task<UpladImageResults> UploadFilesAsync(
        string pathOrContainer,
        List<FileDto> files,
        CancellationToken cancellationToken
    ) => _storage.UploadFilesAsync(pathOrContainer, files, cancellationToken);
}
