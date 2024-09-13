using Microsoft.AspNetCore.Http;

namespace EticaretAPI.Application.Abstractions.Storage;

public interface IStorage
{
	Task<List<(string fileName, string pathOrContainer)>> UploadFilesAsync(
		string pathOrContainer,
		IFormFileCollection files
	);
	Task<bool> DeleteFileAsync(string pathOrContainer , string fileName);

	List<string> GetFiles(string pathOrContainer);

}
