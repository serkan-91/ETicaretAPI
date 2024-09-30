using EticaretAPI.Application.Features.Commands.ProductImageFile.Upload;

namespace EticaretAPI.API.Helpers.Common;

public class FileService
{
	public static List<FileData> ConvertToFileDtos(IFormFileCollection files)
	{
		var fileDtos = new List<FileData>();

		foreach (IFormFile file in files)
		{
			using var memoryStream = new MemoryStream();
			file.CopyTo(memoryStream);

			FileData fileDto =
				new()
				{
					FileName = file.FileName,
					Content = memoryStream.ToArray(),
					ContentType = file.ContentType,
				};

			fileDtos.Add(fileDto);
		}

		return fileDtos;
	}
}
