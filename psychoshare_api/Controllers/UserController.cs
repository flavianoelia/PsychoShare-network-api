using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.User;
using System.Text.RegularExpressions;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private DAOFactory? df;

    public UserController(ILogger<UserController> logger, DAOFactory df)
    {
        _logger = logger;
        this.df = df; // inyectamos la factoría de DAOs
    }

    // 🔹 Función para validar Name o LastName
    private string? ValidateNameOrLastName(string? value, string fieldName)
    {
        var nameRegex = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{2,30}$");

        if (string.IsNullOrWhiteSpace(value) || !nameRegex.IsMatch(value.Trim()))
        {
            return $"{fieldName} no es válido. Debe tener entre 2 y 30 caracteres y solo letras/espacios.";
        }

        return null; 
    }

    // 🔹 Función para validar Email
    private string? ValidateEmail(string? email)
    {
        var emailRegex = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");

        if (string.IsNullOrWhiteSpace(email) || !emailRegex.IsMatch(email.Trim()))
        {
            return "El email no tiene un formato válido.";
        }

        return null; 
    }

    // 🔹 Función que agrupa las validaciones del registro
    private List<string> ValidateUserFields(CreateUserRequestDTO req)
    {
        var errores = new List<string>();

        var nameError = ValidateNameOrLastName(req.Name, "El nombre");
        if (nameError != null) errores.Add(nameError);

        var lastNameError = ValidateNameOrLastName(req.LastName, "El apellido");
        if (lastNameError != null) errores.Add(lastNameError);

        var emailError = ValidateEmail(req.Email);
        if (emailError != null) errores.Add(emailError);

        return errores;
    }

    // 🔹 POST: Register
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] CreateUserRequestDTO req)
    {
        var errores = ValidateUserFields(req);

        if (errores.Any())
            return BadRequest(new { success = false, errors = errores });

        var existingUser = df?.DAOUser().GetUserByEmail(req.Email!.Trim());
        if (existingUser != null)
            return Conflict(new { success = false, message = "El email ya está registrado." });

        // Crear usuario y guardar usando SaveAsync
        var user = new entity_library.system.User
        {
            Name = req.Name!,
            LastName = req.LastName!,
            Email = req.Email!,
            PasswordHash = entity_library.system.User.HashPassword(req.Password!)
        };

        if (df != null)
        {
            // BP
            await df.DAOUser().SaveAsync(user);
        }

        return Ok(new { success = true, message = "Usuario registrado y guardado." });
    }

    [HttpGet("login")]
    public void Login()
    {
        // TODO: Implement user login
    }

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
    public IActionResult CheckEmail([FromQuery] string email)
    {
        var user = df?.DAOUser().GetUserByEmail(email);
        return Ok(new { exists = user != null });
    }
}
