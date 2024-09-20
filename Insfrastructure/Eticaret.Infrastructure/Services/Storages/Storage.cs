using EticaretAPI.Infrastructure.Operations;

namespace EticaretAPI.Infrastructure.Services.Storages;

public abstract class Storage
{
    protected abstract Task<bool> HasFileAsync(
        string pathOrContainerName,
        string fileName,
        CancellationToken cancellationToken
    );

    protected async Task<string> FileRenameAsync(
        string pathOrContainerName,
        string fileName,
        CancellationToken cancellationToken
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        string extension = Path.GetExtension(fileName);
        string oldFileName = Path.GetFileNameWithoutExtension(fileName);
        string sanitizedFileName = NameOperation.CharacterRegularity(oldFileName);
        string newFileName = $"{sanitizedFileName}{extension}";
        int suffix = -2;

        while (
            await HasFileAsync(pathOrContainerName, newFileName, cancellationToken)
                .ConfigureAwait(false)
        )
        {
            cancellationToken.ThrowIfCancellationRequested();
            newFileName = $"{sanitizedFileName}{suffix}{extension}";
            suffix--;
        }

        return newFileName;
    }
}
