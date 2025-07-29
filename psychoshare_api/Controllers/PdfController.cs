using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PdfController : ControllerBase
{
    private readonly ILogger<PdfController> _logger;

    public PdfController(ILogger<PdfController> logger)
    {
        _logger = logger;
    }

    [HttpPost("upload")]
    public void AddPdf()
    {
        // TODO: Upload PDF file
    }

    [HttpGet("{id}")]
    public void GetPdf(int id)
    {
        // TODO: Get PDF by ID
    }

    [HttpGet("user/{userId}")]
    public void GetUserPdfs(int userId)
    {
        // TODO: Get PDFs by user
    }

    [HttpDelete("{id}")]
    public void DeletePdf(int id)
    {
        // TODO: Delete PDF
    }
}
