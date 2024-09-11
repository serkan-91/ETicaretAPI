using Microsoft.AspNetCore.Http;

namespace EticaretAPI.Application.Operations;

    public interface IFileService
    {
    Task<List<(string fileName, string path)>> UploadFilesAsync(string path, IFormFileCollection files );
    Task<bool> CopyFileAsync(string path, IFormFile file);
}

