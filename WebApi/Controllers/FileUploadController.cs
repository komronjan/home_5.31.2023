namespace WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.IO;
[ApiController]
[Route("[controller]")]

public class FileUploadController : ControllerBase
{
    readonly IWebHostEnvironment _webHostEnvironment;
    public FileUploadController(IWebHostEnvironment _webHostEnvironment) => this._webHostEnvironment = _webHostEnvironment;
    //get Files
    [HttpGet("GetFiles")]
    public List<string> GetFiles(string _path)
    {
        var list = new List<string>();
        var path = Path.Combine(_webHostEnvironment.WebRootPath, _path);
        var files = Directory.GetFiles(path);
        list.AddRange(files);
        var directories = Directory.GetDirectories(path);
        list.AddRange(directories);
        return list.ToList();
    }
    //Upload File
    [HttpPost("UploadFile")]
    public bool FileUpload(IFormFile file, string _file)
    {
        var current = this._webHostEnvironment.WebRootPath;
        var fullPath = Path.Combine(current, _file, file.FileName);
        var path = Path.Combine(current, _file);


        if (!System.IO.Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        using (var stream = new FileStream(fullPath, FileMode.OpenOrCreate))
        {
            file.CopyTo(stream);
        }
        return true;


    }
    //Delete File
    [HttpDelete("DeleteFile")]
    public bool DeleteFile(string folderName, string fileName)
    {
        var current = this._webHostEnvironment.WebRootPath;
        var fullPath = Path.Combine(current, folderName, fileName);
        if (System.IO.File.Exists(fullPath))
        {
            System.IO.File.Delete(fullPath);
            return true;
        }
        else
        {
            return false;
        }
    }
}
