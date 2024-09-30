using EticaretAPI.Application.Features.Commands.ProductImageFile.Upload;
using EticaretAPI.Application.ResponseParameters;

namespace EticaretAPI.Application.Abstractions.Storage;

public interface IStorage
{
	Task<List<UploadProductImageCommandResponse>> UploadFilesAsync(
		string pathOrContainer,
		List<FileData> files,
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
