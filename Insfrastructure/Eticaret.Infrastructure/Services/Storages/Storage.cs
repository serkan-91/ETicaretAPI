using EticaretAPI.Infrastructure.Operations;

namespace EticaretAPI.Infrastructure.Services.Storages;

public class Storage
{
    protected delegate bool HasFile(string pathOrContainer , string path);
    protected static async Task<string> FileNameFormatConverterAsync(string pathOrContainer, string fileName, HasFile hasFileMethod)
    {
        await Task.Run(() =>
        {
            string baseName = Path.GetFileNameWithoutExtension(fileName);  
            string extension = Path.GetExtension(fileName);


            int increment = 2;

            fileName = NameOperation.CharacterRegulatory(fileName);

            while (hasFileMethod(pathOrContainer, fileName))
            {
                baseName = baseName.Replace($"-{increment - 1}" , $"-{increment}");
                fileName = baseName + extension;
                increment++;

            }
             

        });
        return fileName;
    }
}
