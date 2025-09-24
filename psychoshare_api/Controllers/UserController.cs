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

    // 🔹 Validaciones reutilizables
    private List<string> ValidateUserFields(CreateUserRequestDTO req)
    {
        var errores = new List<string>();

        // Regex iguales al front
        var nameRegex = new Regex(@"^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]{2,30}$");
        var emailRegex = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$");
        var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$");

        // --- Name ---
        if (string.IsNullOrWhiteSpace(req.Name) || !nameRegex.IsMatch(req.Name.Trim()))
            errores.Add("El nombre no es válido. Debe tener entre 2 y 30 caracteres y solo letras/espacios.");

        // --- LastName ---
        if (string.IsNullOrWhiteSpace(req.LastName) || !nameRegex.IsMatch(req.LastName.Trim()))
            errores.Add("El apellido no es válido. Debe tener entre 2 y 30 caracteres y solo letras/espacios.");

        return errores;
    }

    // 🔹 POST: Register
    [HttpPost]
    public IActionResult Register([FromBody] CreateUserRequestDTO req)
    {
        var errores = ValidateUserFields(req);

        if (errores.Any())
            return BadRequest(new { success = false, errors = errores });

        // TODO: Guardar usuario usando df.DAOUser()
        return Ok(new { success = true, message = "Usuario válido." });
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

    [HttpGet("search/{username}")]
    public IActionResult SearchUser(string username)
    {
        var user = new User { Username = username, Name = "Mock", LastName = "User", Email = "mock@user.com" };
        return Ok(new { success = true, content = user });
    }

    [HttpGet("check-email")]
    public IActionResult CheckEmail([FromQuery] string email)
    {
        var user = df?.DAOUser().GetUserByEmail(email);
        return Ok(new { exists = user != null });
    }
}
