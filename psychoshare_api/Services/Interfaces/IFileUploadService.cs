using Microsoft.AspNetCore.Http;
namespace psychoshare_api.Services.Interfaces;
public interface IFileUploadService
{
    public string SaveAvatar(IFormFile file);
    public string SaveImage(IFormFile file);
    public void DeleteFileByUrl(string url);
}
