using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EticaretAPI.Application.Abstractions.Storage;

namespace EticaretAPI.Infrastructure.Services.Storage;

public class StorageService(IStorage _storage) : IStorageService
{
	public async Task<bool> DeleteFileAsync(string pathOrContainer, string fileName) =>
		await _storage.DeleteFileAsync(pathOrContainer, fileName);

	public List<string> GetFiles(string pathOrContainer) => _storage.GetFiles(pathOrContainer);

	public bool HasFile(string pathOrContainer, string fileName) =>
		_storage.HasFile(pathOrContainer, fileName);

	public async Task<List<(string fileName, string pathOrContainer)>> UploadFilesAsync(
		string pathOrContainer,
		IFormFileCollection files
	) => await _storage.UploadFilesAsync(pathOrContainer, files);
}
