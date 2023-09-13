namespace EticaretAPI.Infrastructure.Services;
using EticaretAPI.Infrastructure.Operations;

public class FileService  
{ 
    static async Task<string> FileNameFormatConverterAsync(string path, string fileName)
    {
        await Task.Run(() =>
         {
             string baseName = Path.GetFileNameWithoutExtension(fileName);
             string extension = Path.GetExtension(fileName);


             int increment = 2;
             
             fileName = NameOperation.CharacterRegulatory(fileName);

             while (File.Exists(fileName))
             {
                 baseName = baseName.Replace($"-{increment - 1}", $"-{increment}");
                 fileName = baseName + extension;
                 increment++;

             }

             return fileName;




         });
        return "";
    }

} 
