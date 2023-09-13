namespace EticaretAPI.Infrastructure.Services;
using EticaretAPI.Application.Services;
using EticaretAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

public class FileService : IFileService
{
    readonly private IWebHostEnvironment _webHostEnvironment;
    public FileService(IWebHostEnvironment webHostEnvironment = null)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<bool> CopyFileAsync(string path, IFormFile file)
    {
        try
        {
            await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

            await file.CopyToAsync(fileStream);
            await fileStream.FlushAsync();
            return true;

        }
        catch (Exception ex)
        {
            //todo log!
            throw ex;
        }


    }


    public async Task<List<(string fileName, string path)>> UploadAsync(string path, IFormFileCollection files)
    {
        string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);

        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        List<(string fileName, string path)> datas = new();
        List<bool> results = new();

        foreach (IFormFile file in files)
        {
            string fileFormatedName = await FileNameFormatConverterAsync(uploadPath, file.FileName);

            bool result = await CopyFileAsync($"{uploadPath}\\{fileFormatedName}", file);
            datas.Add((fileFormatedName, $"{path}\\{fileFormatedName}"));
            results.Add(result);
        }
        if (results.TrueForAll(r => r.Equals(true)))
            return datas;

        return null;
        //todo Eger ki yukaridaki if gecerli degilse burada dosyalarin sunucuda yuklenirken hata alindigina dair uyarici bir exception olusturup firlatilmasi gerekiyor!
    }

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
