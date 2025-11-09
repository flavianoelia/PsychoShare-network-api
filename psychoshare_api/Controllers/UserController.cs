using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.User;
using System.Text.RegularExpressions;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly DAOFactory _daoFactory;
    private readonly TokenService _tokenService;


    public UserController(ILogger<UserController> logger, DAOFactory daoFactory, TokenService tokenService)
    {
        _logger = logger;
        _daoFactory = daoFactory;
        _tokenService = tokenService;
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
            PasswordHash = entity_library.system.User.HashPassword(req.Password!)
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

        var response = new
        {
            Id = user.Id,
            user.Name,
            user.LastName,
            user.Email,
            ProfilePictureUrl = user.Image?.Url,
            RoleName = user.Role?.RoleName
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

    if (!string.IsNullOrWhiteSpace(req.ProfilePictureUrl))
    {
        if (user.Image == null)
            user.Image = new Image();

        user.Image.Url = req.ProfilePictureUrl.Trim();
    }

    _daoFactory.DAOUser().UpdateUser(id);

    
    var response = new UserResponseDto
    {
        Id = user.Id,
        Name = user.Name,
        LastName = user.LastName,
        Email = user.Email,
        RoleName = user.Role?.RoleName ?? "",
        ImageUrl = user.Image?.Url ?? ""
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

            var userDtos = users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                LastName = u.LastName,
                Email = u.Email,
                RoleName = u.Role?.RoleName,
                ImageUrl = u.Image?.Url
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
}
