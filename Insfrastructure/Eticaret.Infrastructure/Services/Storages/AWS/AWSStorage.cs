using EticaretAPI.Application.Abstractions.Storage.AWS;
using EticaretAPI.Application.Features.Commands.ProductImageFile.Upload;

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

	public Task<List<UploadProductImageCommandResponse>> UploadFilesAsync(
		string pathOrContainer,
		List<FileData> fileNames,
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
