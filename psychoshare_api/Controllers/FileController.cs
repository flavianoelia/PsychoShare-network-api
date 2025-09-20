using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly ILogger<FileController> _logger;

    public FileController(ILogger<FileController> logger)
    {
        _logger = logger;
    }

    [HttpPost("upload")]
    public void UploadFile()
    {
        // TODO: Upload file
    }

    [HttpGet("{id}")]
    public void GetFile(int id)
    {
        // TODO: Get file by ID
    }

    [HttpGet("user/{userId}")]
    public void GetUserFiles(int userId)
    {
        // TODO: Get all files by user
    }

    [HttpDelete("{id}")]
    public void DeleteFile(int id)
    {
        // TODO: Delete file
    }
}
