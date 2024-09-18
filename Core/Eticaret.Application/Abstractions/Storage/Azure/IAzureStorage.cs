using Azure.Storage.Blobs;

namespace EticaretAPI.Application.Abstractions.Storage.Azure;

public interface IAzureStorage : IStorage
	{
	BlobServiceClient GetBlobClient();
	}