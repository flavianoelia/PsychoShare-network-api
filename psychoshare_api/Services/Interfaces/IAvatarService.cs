using Microsoft.AspNetCore.Http;
using psychoshare_api.DTOs.Media.AvatarDto;

namespace psychoshare_api.Services.Interfaces;

public interface IAvatarService
{
    public AvatarResponseDTO UpsertAvatar(long userId, IFormFile file);
    public AvatarResponseDTO? GetUserAvatar(long userId);
    public void DeleteAvatar(long userId);
}
