using EticaretAPI.Application.Common.Dtos;
using EticaretAPI.Application.ResponseParameters;

namespace EticaretAPI.Application.Abstractions.Storage;

public interface IStorage
{
    Task<UpladImageResults> UploadFilesAsync(
        string pathOrContainer,
        List<FileDto> files,
        CancellationToken cancellationToken
    );

    Task<bool> DeleteFileAsync(
        string pathOrContainer,
        string fileName,
        CancellationToken cancellationToken
    );

    Task<List<string>> GetFilesAsync(string pathOrContainer, CancellationToken cancellationToken);

    string GetBasePathOrContainer { get; }
}
