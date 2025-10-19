using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.User;
using System.Text.RegularExpressions;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly DAOFactory _daoFactory;

    public UserController(ILogger<UserController> logger, DAOFactory daoFactory)
    {
        _logger = logger;
        _daoFactory = daoFactory;
    }

    #region 🔹 Validaciones privadas

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

    private List<string> ValidateUserFields(CreateUserRequestDTO req)
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

    #region 🔹 Endpoints públicos

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] CreateUserRequestDTO req)
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

        return Ok(new { success = true, message = "Usuario registrado y guardado." });
    }

    // GET: api/User/login
    [HttpGet("login")]
    public void Login()
    {
        // TODO: Implement user login
    }

    // GET: api/User/{id}
    [HttpGet("{id:long}")]
    public void GetUser(long id)
    {
        // TODO: Implement user getUser
    }

    // PUT: api/User/edit/{id}
    [HttpPut("edit/{id:long}")]
    public IActionResult EditProfile(long id)
    {
        // TODO: Edit user profile
        return Ok();
    }

    // GET: api/User/check-email?email=example@test.com
    [HttpGet("check-email")]
    public IActionResult CheckEmail([FromQuery] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return BadRequest("El parámetro 'email' es obligatorio.");

        var user = _daoFactory.DAOUser().GetUserByEmail(email.Trim());
        return Ok(new { exists = user != null });
    }

    #endregion
}
