using EticaretAPI.Application.Abstractions.Storage.AWS;

namespace EticaretAPI.Infrastructure.Services.Storages.AWS;

public class AWSStorage : Storage, IAWSStorage
{
	public Task<bool> DeleteFileAsync(string pathOrContainer, string fileName)
	{
		throw new NotImplementedException();
	}

	public List<string> GetFiles(string pathOrContainer)
	{
		throw new NotImplementedException();
	}

	 

	public Task<List<(string fileName, string pathOrContainer)>> UploadFilesAsync(
		string pathOrContainer,
		IFormFileCollection files
	)
	{
		throw new NotImplementedException();
	}

	protected override bool HasFile(string pathOrContainerName , string fileName)
		{
		throw new NotImplementedException();
		}
	}
