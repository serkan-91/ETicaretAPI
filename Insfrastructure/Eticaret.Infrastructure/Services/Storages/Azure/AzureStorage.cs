using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EticaretAPI.Application.Abstractions.Storage;
using EticaretAPI.Application.Abstractions.Storage.Azure;
using static EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.Infrastructure.Services.Storages.Azure;

public class AzureStorage : Storage, IAzureStorage
	{
	private readonly BlobServiceClient _blobServiceClient = new(Configurations.GetCurrentStorage);

	private BlobContainerClient _blobContainerClient;

	public BlobServiceClient GetBlobClient() => _blobServiceClient;

	public StorageType StorageServiceType => StorageType.Azure;

	public async Task<List<(string fileName, string pathOrContainer)>> UploadFilesAsync(
		string containerName ,
		List<string> files
	)
		{
		List<(string fileName, string pathOrContainer)> datas = [];

		_blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
		await _blobContainerClient.CreateIfNotExistsAsync();
		await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

		foreach ( string filePath in files )
			{
			using var stream = new FileStream(filePath , FileMode.Open , FileAccess.Read);
			string uniqueFileName = await FileRenameAsync(containerName , Path.GetFileName(filePath));

			await _blobContainerClient.GetBlobClient(uniqueFileName).UploadAsync(stream , true);
			datas.Add((Path.GetFileName(filePath), $"{containerName}/{uniqueFileName}"));
			}
		return datas;
		}

	public async Task<bool> DeleteFileAsync(string containerName , string fileName) =>
		await _blobServiceClient
			.GetBlobContainerClient(containerName)
			.GetBlobClient(fileName)
			.DeleteIfExistsAsync();

	public List<string> GetFiles(string containerName)
		{
		_blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
		return _blobContainerClient.GetBlobs().Select(blob => blob.Name).ToList();
		}

	protected override async Task<bool> HasFileAsync(string containerName , string fileName)
		{
		BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(
			containerName
		);
		BlobClient blobClient = containerClient.GetBlobClient(fileName);
		return await blobClient.ExistsAsync();
		}
	}