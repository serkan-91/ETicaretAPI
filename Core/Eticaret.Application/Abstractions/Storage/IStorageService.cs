using static EticaretAPI.Domain.Entities.File;

namespace EticaretAPI.Application.Abstractions.Storage;

public interface IStorageService : IStorage
{
	StorageType StorageServiceType { get; }
}
