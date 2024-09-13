using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EticaretAPI.Application.Helper;
using EticaretAPI.Infrastructure.Operations;

namespace EticaretAPI.Infrastructure.Services.Storages;

public abstract class Storage
{
	protected abstract bool HasFile(string pathOrContainerName, string fileName);

	protected async Task<string> FileRenameAsync(
		string pathOrContainerName,
		string fileName 
	) =>
		await Task.FromResult(
			result:  ExceptionHandler.Execute(() =>
			{
				string extension = Path.GetExtension(fileName);
				string oldFileName = Path.GetFileNameWithoutExtension(fileName);
				string sanitizedFileName = NameOperation.CharacterRegularity(oldFileName);
				string newFileName = $"{sanitizedFileName}{extension}";
				int suffix = -2;

				while(HasFile(pathOrContainerName , newFileName))
					{
					newFileName = $"{sanitizedFileName}{suffix}{extension}";
					suffix--;
					}

				return newFileName;
			})
		);
}
