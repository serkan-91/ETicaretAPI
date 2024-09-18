﻿using EticaretAPI.Application.Abstractions.Storage.AWS;

namespace EticaretAPI.Infrastructure.Services.Storages.AWS;

public class AWSStorage : Storage, IAWSStorage {
	public string GetBasePathOrContainer => null;

	public Task<bool> DeleteFileAsync(string pathOrContainer , string fileName) {
	throw new NotImplementedException();
		}

	public List<string> GetFiles(string pathOrContainer) {
	throw new NotImplementedException();
		}

	public Task<List<(string fileName, string pathOrContainer)>> UploadFilesAsync(
		string pathOrContainer ,
		List<string> files
	) {
	throw new NotImplementedException();
		}

	protected override Task<bool> HasFileAsync(string pathOrContainerName , string fileName) {
	throw new NotImplementedException();
		}
	}