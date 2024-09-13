using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EticaretAPI.Application.Abstractions.Storage;
using EticaretAPI.Application.Abstractions.Storage.Azure;
using EticaretAPI.Application.Helper;

namespace EticaretAPI.Infrastructure.Services.Storages.Azure
{
	public class AzureStorage : Storage, IAzureStorage
	{
		private BlobServiceClient _blobServiceClient = new(Configurations.GetCurrentStorage);
		BlobContainerClient _blobContainerClient;

		public async Task<List<(string fileName, string pathOrContainer)>> UploadFilesAsync(
			string containerName,
			IFormFileCollection files
		) =>
			await ExceptionHandler.Execute(async () =>
			{
				List<(string fileName, string pathOrContainer)> datas = new();
				_blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
				await _blobContainerClient.CreateIfNotExistsAsync();
				try
				{
					await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Access policy error: {ex.Message}");
				}
				foreach (IFormFile file in files)
				{
					using (var stream = file.OpenReadStream())
					{
						string uniqueFileName = await FileRenameAsync(
							containerName,
							file.FileName 
						);

						await _blobContainerClient
							.GetBlobClient(uniqueFileName)
							.UploadAsync(stream, true);
						datas.Add((file.FileName, $"{containerName}/{uniqueFileName}"));
						}
					}
				return datas;
			});

		public async Task<bool> DeleteFileAsync(string containerName, string fileName) =>
			await _blobServiceClient
				.GetBlobContainerClient(containerName)
				.GetBlobClient(fileName)
				.DeleteIfExistsAsync();

		public List<string> GetFiles(string containerName)
		{
			_blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
			return _blobContainerClient.GetBlobs().Select(blob => blob.Name).ToList();
		}
		protected override  bool HasFile(string containerName , string fileName)
			{
			BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(
		containerName
	);
			BlobClient blobClient = containerClient.GetBlobClient(fileName);
			return blobClient.Exists();
			}

		}
	}
