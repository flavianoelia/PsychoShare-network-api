using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Report;
using entity_library.ReportPolicy;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;
    private readonly DAOFactory _daoFactory;

    public ReportController(ILogger<ReportController> logger, DAOFactory daoFactory)
    {
        _logger = logger;
        _daoFactory = daoFactory;
    }

    private bool IsAdminOrSuperAdmin()
    {
        var roleClaimValue = User.FindFirst(ClaimTypes.Role)?.Value;
        long roleId;
        if (!long.TryParse(roleClaimValue, out roleId))
            roleId = 1;
        return roleId >= 2;
    }

    [HttpPost]
    public ActionResult<ReportResponseDto> ReportUser([FromBody] CreateReportDto createReportDto)
    {
        if (!IsAdminOrSuperAdmin())
            return Forbid();

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
    public ActionResult<ReportPagedResponseDto> GetAllReports([FromQuery] ReportFilterDto? filter = null)
    {
        if (!IsAdminOrSuperAdmin())
            return Forbid();

        try
        {
            filter ??= new ReportFilterDto();

            if (filter.Page < 1) filter.Page = 1;
            if (filter.Size < 1 || filter.Size > 25) filter.Size = 10;

            var (reports, totalCount) = _daoFactory.DAOReport().GetAllPaginated(
                filter.Page,
                filter.Size,
                filter.Status,
                filter.ContentType,
                filter.DateFrom,
                filter.DateTo
            );

            var reportDtos = reports.Select(r => new ReportResponseDto
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

            var response = new ReportPagedResponseDto
            {
                Reports = reportDtos,
                TotalCount = totalCount,
                Page = filter.Page,
                Size = filter.Size,
                HasMore = (filter.Page * filter.Size) < totalCount
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener reportes paginados");
            return StatusCode(500, "Error interno del servidor");
        }
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
