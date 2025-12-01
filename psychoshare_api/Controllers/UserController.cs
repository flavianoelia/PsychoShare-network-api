using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.User;
using System.Text.RegularExpressions;
using entity_library.media;
using psychoshare_api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly DAOFactory _daoFactory;
    private readonly TokenService _tokenService;
    private readonly IFileUploadService _fileUploadService;


    public UserController(ILogger<UserController> logger, DAOFactory daoFactory, TokenService tokenService, IFileUploadService fileUploadService)
    {
        _logger = logger;
        _daoFactory = daoFactory;
        _tokenService = tokenService;
        _fileUploadService = fileUploadService;
    }

    #region Validations
    private bool IsValidNameOrLastName(string? value)
    {
        var nameRegex = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{2,30}$");
        return !string.IsNullOrWhiteSpace(value) && nameRegex.IsMatch(value.Trim());
    }

    private bool IsValidEmail(string? email)
    {
        var emailRegex = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
        return !string.IsNullOrWhiteSpace(email) && emailRegex.IsMatch(email.Trim());
    }

    private bool IsValidPassword(string? password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        return passwordRegex.IsMatch(password);
    }


    private List<string> ValidateUserFields(RegisterRequestDTO req)
    {
        var errores = new List<string>();

        if (!IsValidNameOrLastName(req.Name))
            errores.Add("El nombre no es válido. Debe tener entre 2 y 30 caracteres y solo letras/espacios.");

        if (!IsValidNameOrLastName(req.LastName))
            errores.Add("El apellido no es válido. Debe tener entre 2 y 30 caracteres y solo letras/espacios.");

        if (!IsValidEmail(req.Email))
            errores.Add("El email no tiene un formato válido.");

        if (!IsValidPassword(req.Password))
            errores.Add("La contraseña debe tener al menos 8 caracteres, incluyendo mayúsculas, minúsculas, números y caracteres especiales (@$!%*?&).");

        return errores;
    }
    #endregion

    #region Endpoints
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO req)
    {
        var errores = ValidateUserFields(req);

        if (errores.Any())
            return BadRequest(new { success = false, errors = errores });

        var existingUser = _daoFactory.DAOUser().GetUserByEmail(req.Email!.Trim());
        if (existingUser != null)
            return Conflict(new { success = false, message = "El email ya está registrado." });

        var user = new entity_library.system.User
        {
            Name = req.Name!.Trim(),
            LastName = req.LastName!.Trim(),
            Email = req.Email!.Trim(),
            PasswordHash = entity_library.system.User.HashPassword(req.Password!),
            RoleType = RoleType.User
        };

        await _daoFactory.DAOUser().SaveAsync(user);

        var token = _tokenService.CreateToken(user);
        return Ok(new { success = true, message = "Usuario registrado y guardado.", token = token });
    }

    #endregion

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginRequestDTO req)
    {
        if (!IsValidEmail(req.Email))
        {
            return BadRequest(new { success = false, message = "El email no tiene un formato válido." });
        }
        var user = _daoFactory.DAOUser().GetUserByEmail(req.Email.Trim());
        if (user == null)
        {
            return Unauthorized(new { success = false, message = "Mail o contraseña inválidos" });
        }
        if (!entity_library.system.User.VerifyPassword(req.Password, user.PasswordHash))
        {
            return Unauthorized(new { success = false, message = "Mail o contraseña inválidos" });
        }
        var token = _tokenService.CreateToken(user);

        return Ok(new LoginResponseDTO
        {
            success = true,
            message = "Inicio de sesión exitoso",
            email = user.Email,
            userId = user.Id,
            token = token
        });
    }

    [HttpGet("{id:long}")]
    public IActionResult GetUser(long id)
    {
        var user = _daoFactory.DAOUser().GetUser(id);
        if (user == null)
            return NotFound(new { message = "Usuario no encontrado." });

        var avatar = _daoFactory.DAOAvatar().GetAvatarByUserId(user.Id);
        
        var response = new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            AvatarUrl = avatar?.Url,
            RoleName = user.RoleType.ToString()
        };

        return Ok(response);
    }


    [HttpPut("edit/{id:long}")]
    public IActionResult EditProfile(long id, [FromBody] UpdateUserRequestDto req)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var user = _daoFactory.DAOUser().GetUser(id);
    if (user == null)
        return NotFound(new { message = "Usuario no encontrado." });

    if (!string.IsNullOrWhiteSpace(req.Name))
        user.Name = req.Name.Trim();

    if (!string.IsNullOrWhiteSpace(req.LastName))
        user.LastName = req.LastName.Trim();

    if (!string.IsNullOrWhiteSpace(req.Email))
        user.Email = req.Email.Trim();

    // Update avatar using DAO (User.Avatar is Ignored in EF)
    if (!string.IsNullOrWhiteSpace(req.ProfilePictureUrl))
    {
        var existingAvatar = _daoFactory.DAOAvatar().GetAvatarByUserId(user.Id);
        if (existingAvatar == null)
        {
            // Create new avatar
            var newAvatar = new Avatar
            {
                IdUser = user.Id,
                Url = req.ProfilePictureUrl.Trim()
            };
            _daoFactory.DAOAvatar().Save(newAvatar);
        }
        else
        {
            // Update existing avatar
            existingAvatar.Url = req.ProfilePictureUrl.Trim();
            _daoFactory.DAOAvatar().UpdateAvatarByUserId(user.Id, existingAvatar);
        }
    }

    _daoFactory.DAOUser().UpdateUser(id);

    var avatar = _daoFactory.DAOAvatar().GetAvatarByUserId(user.Id);
    
    var response = new UserResponseDto
    {
        Id = user.Id,
        Name = user.Name,
        LastName = user.LastName,
        Email = user.Email,
        RoleName = user.RoleType.ToString(),
        AvatarUrl = avatar?.Url ?? ""
    };

    return Ok(new
    {
        success = true,
        message = "Perfil actualizado correctamente.",
        user = response
    });
}


    [HttpGet("check-email")]
    [AllowAnonymous]
    public IActionResult CheckEmail([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest("El parámetro 'email' es obligatorio.");

        var user = _daoFactory.DAOUser().GetUserByEmail(email.Trim());
        return Ok(new { exists = user != null });
    }

    [HttpGet("all")]
    public IActionResult GetAllUsers([FromQuery] UserFilterDto? filter = null)
    {
        try
        {
            filter ??= new UserFilterDto();

            if (filter.Page < 1) filter.Page = 1;
            if (filter.Size < 1 || filter.Size > 25) filter.Size = 10;

            var (users, totalCount) = _daoFactory.DAOUser().GetAllPaginated(
                filter.Page,
                filter.Size,
                filter.Search,
                filter.Role
            );

            var userDtos = users.Select(u => {
                var avatar = _daoFactory.DAOAvatar().GetAvatarByUserId(u.Id);
                return new UserResponseDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    LastName = u.LastName,
                    Email = u.Email,
                    RoleName = u.RoleType.ToString(),
                    AvatarUrl = avatar?.Url
                };
            }).ToList();

            var response = new UserPagedResponseDto
            {
                Users = userDtos,
                TotalCount = totalCount,
                Page = filter.Page,
                Size = filter.Size,
                HasMore = (filter.Page * filter.Size) < totalCount
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener usuarios paginados");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPatch("{id:long}/role")]
    public IActionResult UpdateUserRole(long id, [FromBody] UpdateRoleDto req)
    {
        try
        {
            var user = _daoFactory.DAOUser().GetUser(id);
            if (user == null)
                return NotFound(new { success = false, message = "Usuario no encontrado." });

            if (!Enum.IsDefined(typeof(RoleType), req.RoleType))
                return BadRequest(new { success = false, message = "Rol inválido." });

            user.RoleType = (RoleType)req.RoleType;
            _daoFactory.DAOUser().UpdateUser(id);

            return Ok(new
            {
                success = true,
                message = "Rol actualizado correctamente.",
                roleName = user.RoleType.ToString()
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar rol del usuario");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPut("change-password/{id:long}")]
    public IActionResult ChangePassword(long id, [FromBody] ChangePasswordDto req)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            var user = _daoFactory.DAOUser().GetUser(id);
            if (user == null)
                return NotFound(new { success = false, message = "Usuario no encontrado." });

            // Verificar que la contraseña actual sea correcta
            if (!entity_library.system.User.VerifyPassword(req.OldPassword, user.PasswordHash))
                return BadRequest(new { success = false, message = "La contraseña actual es incorrecta." });

            // Validar que la nueva contraseña cumpla los requisitos
            if (!IsValidPassword(req.NewPassword))
                return BadRequest(new { success = false, message = "La nueva contraseña debe tener al menos 8 caracteres, incluyendo mayúsculas, minúsculas, números y caracteres especiales (@$!%*?&)." });

            // Actualizar la contraseña
            user.PasswordHash = entity_library.system.User.HashPassword(req.NewPassword);
            _daoFactory.DAOUser().UpdateUser(id);

            return Ok(new { success = true, message = "Contraseña actualizada correctamente." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cambiar contraseña del usuario");
            return StatusCode(500, new { success = false, message = "Error interno del servidor" });
        }
    }

    /// <summary>
    /// Elimina un usuario y todos sus datos relacionados en cascada (posts, likes, comments, followings, archivos).
    /// Solo el propio usuario o un Admin pueden eliminar la cuenta.
    /// </summary>
    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteUser(long id)
    {
        try
        {
            // Obtener ID del usuario autenticado desde el JWT
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out long currentUserId))
            {
                return Unauthorized(new { success = false, message = "No autorizado. Token inválido." });
            }

            // Obtener rol del usuario autenticado
            var roleClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            bool isSuperadmin = roleClaim == RoleType.Superadmin.ToString();
            bool isAdmin = roleClaim == RoleType.Admin.ToString();

            // Verificar autorización:
            // - Usuario común (User): solo puede eliminarse a sí mismo
            // - Admin: puede eliminar usuarios comunes y a sí mismo
            // - Superadmin: puede eliminar a CUALQUIERA (users, admins, otros superadmins)
            if (!isSuperadmin)
            {
                // Si no es Superadmin, solo puede eliminar a sí mismo o (si es Admin) a usuarios de menor rango
                if (currentUserId != id && !isAdmin)
                {
                    return StatusCode(403, new { success = false, message = "No tienes permiso para eliminar este usuario." });
                }

                // Si es Admin pero intenta eliminar a otro Admin o Superadmin, denegar
                if (isAdmin && currentUserId != id)
                {
                    var userToCheck = _daoFactory.DAOUser().GetUser(id);
                    if (userToCheck != null && (userToCheck.RoleType == RoleType.Admin || userToCheck.RoleType == RoleType.Superadmin))
                    {
                        return StatusCode(403, new { success = false, message = "Los Admins solo pueden eliminar usuarios comunes." });
                    }
                }
            }

            // Obtener usuario con includes para recuperar URLs de archivos ANTES de eliminar
            var userToDelete = _daoFactory.DAOUser().GetUser(id);
            if (userToDelete == null)
            {
                return NotFound(new { success = false, message = "Usuario no encontrado." });
            }

            // Obtener todas las URLs de archivos físicos ANTES de eliminar de DB
            List<string> fileUrlsToDelete = new List<string>();

            // Avatar
            var avatar = _daoFactory.DAOAvatar().GetAvatarByUserId(id);
            if (avatar != null && !string.IsNullOrEmpty(avatar.Url))
            {
                fileUrlsToDelete.Add(avatar.Url);
            }

            // Images - acceso directo a EFDAOFactory
            if (_daoFactory is dao_library.interfaces.media.EFDAOFactory efFactory)
            {
                var images = await efFactory.DaoImage().GetByUserIdAsync(id);
                if (images != null && images.Any())
                {
                    fileUrlsToDelete.AddRange(images.Where(i => !string.IsNullOrEmpty(i.Url)).Select(i => i.Url));
                }

                // PDFs
                var pdfs = await efFactory.DaoPdf().GetByUserIdAsync(id);
                if (pdfs != null && pdfs.Any())
                {
                    fileUrlsToDelete.AddRange(pdfs.Where(p => !string.IsNullOrEmpty(p.Url)).Select(p => p.Url));
                }
            }

            // Eliminar usuario de la base de datos (CASCADE eliminará automáticamente todos los registros relacionados)
            _daoFactory.DAOUser().Delete(id);

            // Eliminar archivos físicos del disco DESPUÉS de eliminar de DB (para no dejar archivos huérfanos si falla la DB)
            foreach (var fileUrl in fileUrlsToDelete)
            {
                try
                {
                    _fileUploadService.DeleteFileByUrl(fileUrl);
                }
                catch (Exception fileEx)
                {
                    // Log pero no fallar si un archivo no se puede eliminar
                    _logger.LogWarning(fileEx, "No se pudo eliminar el archivo físico: {FileUrl}", fileUrl);
                }
            }

            _logger.LogInformation("Usuario {UserId} eliminado correctamente con {FileCount} archivos físicos.", id, fileUrlsToDelete.Count);

            return Ok(new
            {
                success = true,
                message = "Usuario y todos sus datos eliminados correctamente.",
                filesDeleted = fileUrlsToDelete.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar usuario {UserId}", id);
            return StatusCode(500, new { success = false, message = "Error interno del servidor al eliminar usuario." });
        }
    }
}
