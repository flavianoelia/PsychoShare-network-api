using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using psychoshare_api.Services.Interfaces;
using psychoshare_api.DTOs.Media.AvatarDto;
using dao_library;
using dao_library.interfaces.media;
using entity_library.media;
using dao_library.Contexts;

namespace psychoshare_api.Services;

public class AvatarService : IAvatarService
{
    private readonly DAOFactory _daoFactory;
    private readonly IFileUploadService _fileUploadService;
    private readonly ILogger<AvatarService> _logger;

    public AvatarService(DAOFactory daoFactory, IFileUploadService fileUploadService, ILogger<AvatarService> logger)
    {
        _daoFactory = daoFactory;
        _fileUploadService = fileUploadService;
        _logger = logger;
    }

    public AvatarResponseDTO UploadAvatar(long userId, IFormFile file)
    {
        var user = _daoFactory.DAOUser().GetUser(userId);
        if (user == null) throw new KeyNotFoundException("Usuario no encontrado");

        var existingAvatar = _daoFactory.DAOAvatar().GetAvatarByUserId(userId);

        // 1) Guardar nuevo archivo en disco
        string newUrl;
        try
        {
            newUrl = _fileUploadService.SaveAvatar(file);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al guardar nuevo archivo de avatar");
            throw;
        }

        // 2) Persistir en la DB: si ya existía, eliminar el registro viejo y guardar el nuevo.
        //    Si falla la operación en la DB, intentar eliminar el archivo nuevo (compensación).
        try
        {
            if (existingAvatar != null)
            {
                try
                {
                    _fileUploadService.DeleteFileByUrl(existingAvatar.Url);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al eliminar el avatar viejo");
                }

                _daoFactory.DAOAvatar().DeleteByUserId(userId);
            }

            var newAvatar = new Avatar
            {
                Url = newUrl,
                IdUser = userId,
            };

            _daoFactory.DAOAvatar().Save(newAvatar);

            return new AvatarResponseDTO
            {
                Url = newAvatar.Url,
                FileName = file.FileName,
                ContentType = file.ContentType,
                Size = file.Length
            };
        }
        catch (Exception dbEx)
        {
            _logger.LogError(dbEx, "Error en la DB para guardar/actualizar el avatar. Avatar no actualizado");
            try
            {
                _fileUploadService.DeleteFileByUrl(newUrl);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error compensando el borrado tras fallo DB");
            }
            throw;
        }
    }

    public AvatarResponseDTO UpdateAvatar(long userId, UploadAvatarDto dto)
    {
        // Delegar a UploadAvatar para mantener una única implementación
        return UploadAvatar(userId, dto.File);
    }

    public AvatarResponseDTO? GetUserAvatar(long userId)
    {
        var avatar = _daoFactory.DAOAvatar().GetAvatarByUserId(userId);
        if (avatar == null) return null;

        return new AvatarResponseDTO
        {
            Url = avatar.Url
        };
    }

    public void DeleteAvatar(long userId)
    {
        var avatar = _daoFactory.DAOAvatar().GetAvatarByUserId(userId);
        if (avatar == null) throw new KeyNotFoundException("El usuario no tiene avatar");

        try
        {
            _fileUploadService.DeleteFileByUrl(avatar.Url);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar archivo físico del avatar");
        }

        _daoFactory.DAOAvatar().DeleteByUserId(userId);
    }
}
