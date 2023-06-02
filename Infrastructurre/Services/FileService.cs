using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace Infrastructurre.Services;
public class FileService : IFileService
{
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {

        _environment = environment;
    }
    public string CreateFileName(string folder, IFormFile file)
    {
        var current = _environment.WebRootPath;
        var path = Path.Combine(current, folder, file.FileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        return file.FileName;
    }

    public bool DeleteFile(string folderName, string fileName)
    {
        var path= Path.Combine(_environment.WebRootPath,folderName,fileName);
        if (File.Exists(path)){
            File.Delete(path);
            return true;
        }
        return false;

    }
}
