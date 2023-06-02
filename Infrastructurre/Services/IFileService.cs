namespace Infrastructurre.Services;
using Microsoft.AspNetCore.Http;
public interface IFileService
{
    string CreateFileName(string folder , IFormFile file);
    bool DeleteFile(string folderName,string fileName);
}
