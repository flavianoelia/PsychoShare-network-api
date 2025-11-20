using Microsoft.AspNetCore.Http;
using psychoshare_api.DTOs.Media.AvatarDto;

namespace psychoshare_api.Services.Interfaces;

public interface IAvatarService
{
    public AvatarResponseDTO UploadAvatar(long userId, IFormFile file);
    public AvatarResponseDTO UpdateAvatar(long userId, UploadAvatarDto dto);
    public AvatarResponseDTO? GetUserAvatar(long userId);
    public void DeleteAvatar(long userId);
}
