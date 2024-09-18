using EticaretAPI.Application.Abstractions.Storage;
using static EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.Infrastructure.Services.Storage;

public class StorageService(IStorage _storage , StorageType StorageType) : IStorageService {
	public StorageType StorageServiceType => StorageType;

	public string GetBasePathOrContainer => _storage.GetBasePathOrContainer;

	public async Task<bool> DeleteFileAsync(string pathOrContainer , string fileName) =>
		await _storage.DeleteFileAsync(pathOrContainer , fileName);

	public string GetBasePath() => string.Empty;

	public List<string> GetFiles(string pathOrContainer) => _storage.GetFiles(pathOrContainer);

	public async Task<List<(string fileName, string pathOrContainer)>> UploadFilesAsync(
		string pathOrContainer ,
		List<string> files
	) => await _storage.UploadFilesAsync(pathOrContainer , files);
	}