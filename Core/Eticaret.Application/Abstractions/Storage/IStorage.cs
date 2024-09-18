namespace EticaretAPI.Application.Abstractions.Storage;

public interface IStorage {

	Task<List<(string fileName, string pathOrContainer)>> UploadFilesAsync(
		string pathOrContainer ,
		List<string> fileNames
	);

	Task<bool> DeleteFileAsync(string pathOrContainer , string fileName);

	List<string> GetFiles(string pathOrContainer);
	string GetBasePathOrContainer { get; }
	}