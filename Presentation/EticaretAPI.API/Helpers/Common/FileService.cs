using EticaretAPI.Application.Common.Dtos;

namespace EticaretAPI.API.Helpers.Common;

public class FileService
{
    public static List<FileDto> ConvertToFileDtos(IFormFileCollection files)
    {
        var fileDtos = new List<FileDto>();

        foreach (IFormFile file in files)
        {
            using var memoryStream = new MemoryStream();
            file.CopyTo(memoryStream);

            FileDto fileDto =
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
