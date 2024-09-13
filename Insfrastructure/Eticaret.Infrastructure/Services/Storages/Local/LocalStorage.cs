using EticaretAPI.Application.Abstractions.Storage.Local;
using EticaretAPI.Application.Helper;

namespace EticaretAPI.Infrastructure.Services.Storages.Local;

public class LocalStorage(IWebHostEnvironment _webHostEnvironment) : Storage, ILocalStorage
{

	public List<string> GetFiles(string path)
	{
		string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
		if (Directory.Exists(uploadPath))
		{
			return Directory
				.GetFiles(uploadPath)
				.Select(filePath => Path.GetFileName(filePath))
				.ToList();
		}
		return new List<string>();
	}

	public async Task<List<(string fileName, string pathOrContainer)>> UploadFilesAsync(
		string path,
		IFormFileCollection files
	) =>
		await ExceptionHandler.Execute(
			async () =>
			{
				List<(string fileName, string pathOrContainer)> datas = new();
				string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

				if (!Directory.Exists(uploadPath))
					Directory.CreateDirectory(uploadPath);

				foreach (IFormFile file in files)
				{
					var fileNewName  = await FileRenameAsync(uploadPath,file.FileName );

					string filePath = Path.Combine(uploadPath, fileNewName);
					bool copyResult = await CopyFileAsync(filePath, file);
					if (!copyResult)
						throw new Exception("File Upload Error");

					datas.Add((fileNewName, path));
				}
				return datas;
			},
			"File Upload Operation"
		);

	protected  override bool HasFile(string path, string fileName) =>
		File.Exists(Path.Combine(_webHostEnvironment.WebRootPath, path, fileName));

	public async Task<bool> DeleteFileAsync(string path, string fileName) =>
		await Task.Run(() =>
		{
			string filePath = Path.Combine(_webHostEnvironment.WebRootPath, path, fileName);
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
				return true;
			}
			return false;
		});

	public async Task<bool> CopyFileAsync(string path, IFormFile file) =>
		await ExceptionHandler.Execute(async () =>
		{
			var result = new List<(string fileName, string pathOrContainer)>();
			await using (
				var fileStream = new FileStream(
					path,
					FileMode.Create,
					FileAccess.Write,
					FileShare.None,
					1024 * 1024,
					useAsync: true
				)
			)
			{
				await file.CopyToAsync(fileStream);
				await fileStream.FlushAsync();
			}

			return true;
		});
}
