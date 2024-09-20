using EticaretAPI.Application.Abstractions.Storage.Local;
using EticaretAPI.Application.Common.Dtos;
using EticaretAPI.Application.RequestParameters;
using EticaretAPI.Application.ResponseParameters;
using Microsoft.Extensions.Hosting;
using static EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.Infrastructure.Services.Storages.Local;

public class LocalStorage(IHostEnvironment _hostEnvironment) : Storage, ILocalStorage
{
    public StorageType StorageServiceType => StorageType.Local;

    public string GetBasePathOrContainer => null;

    public Task<List<string>> GetFilesAsync(string path, CancellationToken cancellationToken)
    {
        string uploadPath = Path.Combine(_hostEnvironment.ContentRootPath, path);
        if (Directory.Exists(uploadPath))
        {
            return Task.Run(
                () =>
                    Directory
                        .GetFiles(uploadPath)
                        .Select(filePath => Path.GetFileName(filePath))
                        .ToList(),
                cancellationToken
            );
        }
        return default;
    }

    public async Task<UpladImageResults> UploadFilesAsync(
        string path,
        List<FileDto> files,
        CancellationToken cancellationToken
    )
    {
        UpladImageResults datas = new();
        foreach (var file in files)
        {
            string uniqueFileName = await FileRenameAsync(path, file.FileName, cancellationToken)
                .ConfigureAwait(false);
            if (await HasFileAsync(path, uniqueFileName, cancellationToken).ConfigureAwait(false))
            {
                datas.UpladImages.Add(
                    new UpladImageResult()
                    {
                        FileName = file.FileName,
                        PathOrContainerName = $"{path}/{uniqueFileName}",
                    }
                );
            }
        }
        return datas;
    }

    protected override Task<bool> HasFileAsync(
        string path,
        string fileName,
        CancellationToken cancellationToken
    ) =>
        Task.Run(
            () => File.Exists(Path.Combine(_hostEnvironment.ContentRootPath, path, fileName)),
            cancellationToken
        );

    public Task<bool> DeleteFileAsync(
        string path,
        string fileName,
        CancellationToken cancellationToken
    ) =>
        Task.Run(
            () =>
            {
                string filePath = Path.Combine(_hostEnvironment.ContentRootPath, path, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                return false;
            },
            cancellationToken
        );

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
