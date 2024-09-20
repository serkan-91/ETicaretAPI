using EticaretAPI.Application.Abstractions.Storage.AWS;
using EticaretAPI.Application.Common.Dtos;
using EticaretAPI.Application.ResponseParameters;

namespace EticaretAPI.Infrastructure.Services.Storages.AWS;

public class AWSStorage : Storage, IAWSStorage
{
    public string GetBasePathOrContainer => null;

    public Task<bool> DeleteFileAsync(
        string pathOrContainer,
        string fileName,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetFilesAsync(
        string pathOrContainer,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    public Task<UpladImageResults> UploadFilesAsync(
        string pathOrContainer,
        List<FileDto> fileNames,
        CancellationToken cancellationToken
    )
    {
        throw new NotImplementedException();
    }

    protected override Task<bool> HasFileAsync(
        string pathOrContainerName,
        string fileName,
        CancellationToken cancellation
    )
    {
        throw new NotImplementedException();
    }
}
