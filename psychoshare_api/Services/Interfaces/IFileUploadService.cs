using Microsoft.AspNetCore.Http;
namespace psychoshare_api.Services.Interfaces;
public interface IFileUploadService
{
    string SaveAvatar(IFormFile file);
    string SaveImage(IFormFile file);
    string SavePdf(IFormFile file);
    void DeleteFileByUrl(string url);
}
