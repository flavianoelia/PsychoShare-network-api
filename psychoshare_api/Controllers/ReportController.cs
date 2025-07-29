using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;

    public ReportController(ILogger<ReportController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public void ReportUser()
    {
        // TODO: Report user with name, lastname, and date
    }

    [HttpGet]
    public void GetAllReports()
    {
        // TODO: Get all reports (admin only)
    }

    [HttpGet("{id}")]
    public void GetReport(int id)
    {
        // TODO: Get specific report details
    }

    [HttpPut("{id}/resolve")]
    public void ResolveReport(int id)
    {
        // TODO: Mark report as resolved
    }

    [HttpDelete("{id}")]
    public void DeleteReport(int id)
    {
        // TODO: Delete report
    }
}
