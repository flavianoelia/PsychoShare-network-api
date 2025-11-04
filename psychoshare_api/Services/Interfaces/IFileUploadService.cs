using Microsoft.AspNetCore.Http;
namespace psychoshare_api.Services.Interfaces;

public interface IFileUploadService1
{
    string SaveAvatar(IFormFile file);
}
