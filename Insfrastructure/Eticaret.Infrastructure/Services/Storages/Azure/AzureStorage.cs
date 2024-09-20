using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EticaretAPI.Application.Abstractions.Storage.Azure;
using EticaretAPI.Application.Common.Dtos;
using EticaretAPI.Application.ResponseParameters;
using static EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.Infrastructure.Services.Storages.Azure;

public class AzureStorage : Storage, IAzureStorage
{
    private BlobContainerClient _blobContainerClient;
    private readonly BlobServiceClient _blobServiceClient = new(Configurations.GetCurrentStorage);

    public async Task<List<string>> GetFilesAsync(
        string containerName,
        CancellationToken cancellationToken
    )
    {
        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        List<string> fileNames = new List<string>();

        await foreach (
            BlobItem blobItem in _blobContainerClient.GetBlobsAsync(
                cancellationToken: cancellationToken
            )
        )
        {
            fileNames.Add(blobItem.Name);
        }

        return fileNames;
    }

    protected override async Task<bool> HasFileAsync(
        string containerName,
        string fileName,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(
            containerName
        );
        BlobClient blobClient = containerClient.GetBlobClient(fileName);
        bool exists = await blobClient.ExistsAsync(cancellationToken).ConfigureAwait(false);

        return exists;
    }

    public async Task<bool> DeleteFileAsync(
        string containerName,
        string fileName,
        CancellationToken cancellationToken
    )
    {
        // BlobClient'ı al
        var blobClient = _blobServiceClient
            .GetBlobContainerClient(containerName)
            .GetBlobClient(fileName);

        // Dosyanın silinmesini dene
        bool result = await blobClient
            .DeleteIfExistsAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return result;
    }

    public string GetBasePath()
    {
        throw new NotImplementedException();
    }

    public async Task<UpladImageResults> UploadFilesAsync(
        string containerName,
        List<FileDto> files,
        CancellationToken cancellationToken
    )
    {
        UpladImageResults datas = new UpladImageResults();

        _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await _blobContainerClient
            .CreateIfNotExistsAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        await _blobContainerClient
            .SetAccessPolicyAsync(
                PublicAccessType.BlobContainer,
                cancellationToken: cancellationToken
            )
            .ConfigureAwait(false);

        foreach (FileDto file in files)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var stream = new MemoryStream(file.Content);
            string uniqueFileName = await FileRenameAsync(
                    containerName,
                    file.FileName,
                    cancellationToken
                )
                .ConfigureAwait(false);

            await _blobContainerClient
                .GetBlobClient(uniqueFileName)
                .UploadAsync(stream, true, cancellationToken)
                .ConfigureAwait(false);

            datas.UpladImages.Add(
                new UpladImageResult
                {
                    FileName = uniqueFileName,
                    PathOrContainerName = $"{containerName}/{uniqueFileName}",
                }
            );
        }
        return datas;
    }

    public string GetBasePathOrContainer => _blobServiceClient.Uri.AbsoluteUri;
    public StorageType StorageServiceType => StorageType.Azure;
}
