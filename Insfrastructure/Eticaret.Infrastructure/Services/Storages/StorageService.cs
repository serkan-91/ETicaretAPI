using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EticaretAPI.Application.Abstractions.Storage;
using static EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.Infrastructure.Services.Storage;

public class StorageService(IStorage _storage,StorageType _storageType) :  IStorageService
{
	public StorageType StorageServiceType => _storageType;

	public async Task<bool> DeleteFileAsync(string pathOrContainer, string fileName) =>
		await _storage.DeleteFileAsync(pathOrContainer, fileName);

	public List<string> GetFiles(string pathOrContainer) => _storage.GetFiles(pathOrContainer);


	public async Task<List<(string fileName, string pathOrContainer)>> UploadFilesAsync(
		string pathOrContainer,
		IFormFileCollection files
	) => await _storage.UploadFilesAsync(pathOrContainer, files);
}
