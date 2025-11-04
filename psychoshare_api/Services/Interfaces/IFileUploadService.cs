using Microsoft.AspNetCore.Http;
namespace psychoshare_api.Services.Interfaces;
public interface IFileUploadService
{
    string SaveAvatar(IFormFile file);
}
