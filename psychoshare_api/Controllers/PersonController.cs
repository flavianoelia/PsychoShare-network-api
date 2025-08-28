using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;

    public PersonController(ILogger<PersonController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{id}")]
    public void GetPerson(int id)
    {
        // TODO: Get person details
    }

    [HttpPut("{id}/name")]
    public void UpdateName(int id)
    {
        // TODO: Update person name
    }

    [HttpPut("{id}/lastname")]
    public void UpdateLastName(int id)
    {
        // TODO: Update person last name
    }

    [HttpPut("{id}")]
    public void UpdatePerson(int id)
    {
        // TODO: Update person complete info
    }
}
