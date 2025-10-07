using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Report;
using entity_library.ReportPolicy;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;
    private readonly DAOFactory _daoFactory;

    public ReportController(ILogger<ReportController> logger, DAOFactory daoFactory)
    {
        _logger = logger;
        _daoFactory = daoFactory;
    }

    [HttpPost]
    public ActionResult<ReportResponseDto> ReportUser([FromBody] CreateReportDto createReportDto)
    {
        var report = new Report
        {
            ReporterUserId = createReportDto.ReporterUserId,
            ReportedUserId = createReportDto.ReportedUserId,
            Reason = createReportDto.Reason,
            Details = createReportDto.Details,
            ContentType = createReportDto.ContentType,
            ContentId = createReportDto.ContentId,
            ReportDate = DateTime.Now
        };

        _daoFactory.DAOReport().Save(report);

        var response = new ReportResponseDto
        {
            Id = report.Id,
            ReporterUserId = report.ReporterUserId,
            ReportedUserId = report.ReportedUserId,
            Reason = report.Reason,
            Details = report.Details,
            ReportDate = report.ReportDate,
            Status = report.Status,
            ContentType = report.ContentType,
            ContentId = report.ContentId
        };

        return Ok(response);
    }

    [HttpGet]
    public ActionResult<List<ReportResponseDto>> GetAllReports()
    {
        var reports = _daoFactory.DAOReport().GetAll();
        var response = reports.Select(r => new ReportResponseDto
        {
            Id = r.Id,
            ReporterUserId = r.ReporterUserId,
            ReportedUserId = r.ReportedUserId,
            Reason = r.Reason,
            Details = r.Details,
            ReportDate = r.ReportDate,
            Status = r.Status,
            ContentType = r.ContentType,
            ContentId = r.ContentId
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public ActionResult<ReportResponseDto> GetReport(long id)
    {
        var report = _daoFactory.DAOReport().GetById(id);
        if (report == null)
            return NotFound();

        var response = new ReportResponseDto
        {
            Id = report.Id,
            ReporterUserId = report.ReporterUserId,
            ReportedUserId = report.ReportedUserId,
            Reason = report.Reason,
            Details = report.Details,
            ReportDate = report.ReportDate,
            Status = report.Status,
            ContentType = report.ContentType,
            ContentId = report.ContentId
        };

        return Ok(response);
    }

    [HttpPut("{id}/resolve")]
    public ActionResult<bool> ResolveReport(long id)
    {
        var report = _daoFactory.DAOReport().GetById(id);
        if (report == null)
            return NotFound();

        report.Status = "Resolved";
        _daoFactory.DAOReport().Update(report);

        return Ok(true);
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> DeleteReport(long id)
    {
        var report = _daoFactory.DAOReport().GetById(id);
        if (report == null)
            return NotFound();

        _daoFactory.DAOReport().Delete(report);
        return Ok(true);
    }
}
