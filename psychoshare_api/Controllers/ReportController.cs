using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;
    private readonly IReportRepository _reportRepository;

    public ReportController(ILogger<ReportController> logger, IReportRepository reportRepository)
    {
        _logger = logger;
        _reportRepository = reportRepository;
    }

    [HttpPost]
    public async Task<ActionResult<ReportResponseDto>> ReportUser([FromBody] CreateReportDto createReportDto)
    {
        var report = new Report
        {
            Reason = createReportDto.Name, // Usando Name como Reason (mock)
            Details = createReportDto.Lastname, // Usando Lastname como Details (mock)
            ReportDate = createReportDto.ReportDate,
            Status = "Pending",
            ContentType = "User"
        };
        var id = await _reportRepository.CreateReportAsync(report);
        var response = new ReportResponseDto
        {
            Id = id,
            Name = createReportDto.Name,
            Lastname = createReportDto.Lastname,
            ReportDate = createReportDto.ReportDate,
            IsResolved = false
        };
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<ReportResponseDto>>> GetAllReports()
    {
        var reports = await _reportRepository.GetAllReportsAsync();
        var response = reports.Select(r => new ReportResponseDto
        {
            Id = r.IdReport,
            Name = r.Reason,
            Lastname = r.Details,
            ReportDate = r.ReportDate,
            IsResolved = r.Status == "Resolved"
        }).ToList();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReportResponseDto>> GetReport(long id)
    {
        var report = await _reportRepository.GetReportByIdAsync(id);
        if (report == null)
            return NotFound("Report not found");
        var response = new ReportResponseDto
        {
            Id = report.IdReport,
            Name = report.Reason,
            Lastname = report.Details,
            ReportDate = report.ReportDate,
            IsResolved = report.Status == "Resolved"
        };
        return Ok(response);
    }

    [HttpPut("{id}/resolve")]
    public async Task<ActionResult<bool>> ResolveReport(long id)
    {
    var result = await _reportRepository.ResolveReportAsync(id);
    return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteReport(long id)
    {
    var result = await _reportRepository.DeleteReportAsync(id);
    return Ok(result);
    }
}
