using EticaretAPI.Application.Abstractions.Storage.Local;
using Microsoft.Extensions.Hosting;
using static EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.Infrastructure.Services.Storages.Local;

public class LocalStorage(IHostEnvironment _hostEnvironment) : Storage, ILocalStorage
	{
	public StorageType StorageServiceType => StorageType.Local;

	public List<string> GetFiles(string path)
		{
		string uploadPath = Path.Combine(_hostEnvironment.ContentRootPath, path);
		if(Directory.Exists(uploadPath))
			{
			return Directory
				.GetFiles(uploadPath)
				.Select(filePath => Path.GetFileName(filePath))
				.ToList();
			}
		return [];
		}

	public async Task<List<(string fileName, string pathOrContainer)>> UploadFilesAsync(
		string path ,
		List<string> files
	)
		{
		List<(string fileName, string pathOrContainer)> datas = [];
		foreach(string fileName in files)
			{
			string uniqueFileName = await FileRenameAsync(path, fileName);
			if(await HasFileAsync(path , uniqueFileName))
				{
				datas.Add((fileName, $"{path}/{uniqueFileName}"));
				}
			}
		return datas;
		}

    protected override async Task<bool> HasFileAsync(string path , string fileName) =>
        await Task.Run(() => File.Exists(Path.Combine(_hostEnvironment.ContentRootPath , path , fileName)));

	public async Task<bool> DeleteFileAsync(string path , string fileName) =>
		await Task.Run(() =>
		{
			string filePath = Path.Combine(_hostEnvironment.ContentRootPath, path, fileName);
			if(File.Exists(filePath))
				{
				File.Delete(filePath);
				return true;
				}
			return false;
		});

	//public async Task<bool> CopyFileAsync(string path , IFormFile file) =>
	//	await ExceptionHandler.Execute(async () =>
	//	{
	//		var result = new List<(string fileName, string pathOrContainer)>();
	//		await using var fileStream = new FileStream(
	//				path ,
	//				FileMode.Create ,
	//				FileAccess.Write ,
	//				FileShare.None ,
	//				1024 * 1024 ,
	//				useAsync: true
	//			);
	//		await file.CopyToAsync(fileStream);
	//		await fileStream.FlushAsync();

	//		return true;
	//	});
	}