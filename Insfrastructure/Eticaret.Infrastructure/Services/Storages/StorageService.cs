using EticaretAPI.Application.Abstractions.Storage;
using EticaretAPI.Application.Features.Commands.ProductImageFile.Upload;
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

	public Task<List<UploadProductImageCommandResponse>> UploadFilesAsync(
		string pathOrContainer,
		List<FileData> files,
		CancellationToken cancellationToken
	) => _storage.UploadFilesAsync(pathOrContainer, files, cancellationToken);
}
