using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.User;
using System.Text.RegularExpressions;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private DAOFactory? df;
    private readonly TokenService _tokenService;


    public UserController(ILogger<UserController> logger, DAOFactory df, TokenService tokenService)
    {
        _logger = logger;
        this.df = df;
        _tokenService = tokenService;
    }

    #region validations
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

    #region Register
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO req)
    {
        var errores = ValidateUserFields(req);

        if (errores.Any())
            return BadRequest(new { success = false, errors = errores });

        var existingUser = df?.DAOUser().GetUserByEmail(req.Email!.Trim());
        if (existingUser != null)
            return Conflict(new { success = false, message = "El email ya está registrado." });


        var user = new entity_library.system.User
        {
            Name = req.Name!,
            LastName = req.LastName!,
            Email = req.Email!,
            PasswordHash = entity_library.system.User.HashPassword(req.Password!)
        };

        await df!.DAOUser().SaveAsync(user);

        var token = _tokenService.CreateToken(user);
        return Ok(new { success = true, message = "Usuario registrado y guardado.", token = token });
    }
    #endregion

    #region Login
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginRequestDTO req)
    {
        if (!IsValidEmail(req.Email))
        {
            return BadRequest(new { success = false, message = "El email no tiene un formato válido." });
        }
        var user = df!.DAOUser().GetUserByEmail(req.Email.Trim());
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
            succes = true,
            message = "Inicio de sesión exitoso",
            email = user.Email,
            userId = user.Id,
            token = token
        });
    }
    #endregion

    [HttpGet("{id}")]
    public void GetUser(long id)
    {
        // TODO: Implement user getUser
    }

    [HttpPut("edit/{id}")]
    public IActionResult EditProfile(int id)
    {
        // TODO: Edit user profile
        return Ok();
    }

    [HttpGet("check-email")]
    [AllowAnonymous]
    public IActionResult CheckEmail([FromQuery] string email)
    {
        var user = df?.DAOUser().GetUserByEmail(email);
        return Ok(new { exists = user != null });
    }
}
