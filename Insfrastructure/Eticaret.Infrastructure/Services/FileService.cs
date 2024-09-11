using EticaretAPI.Application.Helper;
using EticaretAPI.Application.Operations;
using EticaretAPI.Infrastructure.Operations;

namespace EticaretAPI.Infrastructure.Services;

public class FileService(IWebHostEnvironment _webHostEnvironment) : IFileService
{
	public async Task<List<(string fileName, string path)>> UploadFilesAsync(
		string subDirectory,
		IFormFileCollection files
	)
	{
		return await ExceptionHandler.Execute(
			async () =>
			{
				string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, subDirectory);

				if (!Directory.Exists(uploadPath))
					Directory.CreateDirectory(uploadPath);

				List<(string fileName, string path)> datas = new();
				List<bool> results = new();

				foreach (IFormFile file in files)
				{
					var newFileName = await FileRenameAsync(uploadPath, file.FileName);
					string filePath = Path.Combine(uploadPath, newFileName);
					bool copyResult = await CopyFileAsync(filePath, file);
					datas.Add((newFileName, filePath));
					results.Add(copyResult);
				}

				if (results.TrueForAll(r => r.Equals(true)))
					return datas;
				return new List<(string fileName, string path)>();
			},
			"File Upload Operation"
		);
	}

	public async Task<bool> CopyFileAsync(string path, IFormFile file)
	{
		return await ExceptionHandler.Execute(async () =>
		{
			var result = new List<(string fileName, string path)>();
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

	async Task<string> FileRenameAsync(string path, string fileName)
	{
		string extension = Path.GetExtension(fileName);
		string oldFileName = Path.GetFileNameWithoutExtension(fileName);
		string newFileName = $"{NameOperation.CharacterRegularity(oldFileName)}{extension}";

		while (File.Exists($"{path}\\{newFileName}"))
		{
			int counter = 1;
			newFileName =
				$"{Path.GetFileNameWithoutExtension(newFileName)}-{counter.ToString()}{extension}";
			counter++;
		}
		return await Task.FromResult(newFileName);
	}
}
