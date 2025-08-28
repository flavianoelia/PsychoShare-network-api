using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : ControllerBase
{
    private readonly ILogger<ImageController> _logger;

    public ImageController(ILogger<ImageController> logger)
    {
        _logger = logger;
    }

    [HttpPost("upload")]
    public void AddImage()
    {
        // TODO: Upload image
    }

    [HttpGet("{id}")]
    public void GetImage(int id)
    {
        // TODO: Get image by ID
    }

    [HttpGet("user/{userId}")]
    public void GetUserImages(int userId)
    {
        // TODO: Get images by user
    }

    [HttpDelete("{id}")]
    public void DeleteImage(int id)
    {
        // TODO: Delete image
    }
}
