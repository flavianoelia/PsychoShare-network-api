public interface IReportService
{
    Task<ReportResponseDto> CreateReportAsync(CreateReportDto createReportDto);
    Task<ReportResponseDto?> GetReportByIdAsync(long reportId);
    Task<List<ReportResponseDto>> GetAllReportsAsync();
    Task<bool> ResolveReportAsync(long reportId);
    Task<bool> DeleteReportAsync(long reportId);
}

public class ReportService : IReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService(IReportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public Task<ReportResponseDto> CreateReportAsync(CreateReportDto createReportDto)
    {
        var report = new Report
        {
            Reason = createReportDto.Name, // Using Name as Reason (mock)
            Details = createReportDto.Lastname, // Using Lastname as Details (mock)
            ReportDate = createReportDto.ReportDate,
            Status = "Pending",
            ContentType = "User"
        };
        return _reportRepository.CreateReportAsync(report)
            .ContinueWith(task =>
            {
                var id = task.Result;
                return new ReportResponseDto
                {
                    Id = id,
                    Name = createReportDto.Name,
                    Lastname = createReportDto.Lastname,
                    ReportDate = createReportDto.ReportDate,
                    IsResolved = false
                };
            });
    }

    public Task<ReportResponseDto?> GetReportByIdAsync(long reportId)
    {
        return _reportRepository.GetReportByIdAsync(reportId)
            .ContinueWith(task =>
            {
                var report = task.Result;
                if (report == null)
                    return null;
                return new ReportResponseDto
                {
                    Id = report.IdReport,
                    Name = report.Reason, // Using Reason as Name (mock)
                    Lastname = report.Details, // Using Details as Lastname (mock)
                    ReportDate = report.ReportDate,
                    IsResolved = report.Status == "Resolved"
                };
            });
    }

    public Task<List<ReportResponseDto>> GetAllReportsAsync()
    {
        return _reportRepository.GetAllReportsAsync()
            .ContinueWith(task =>
            {
                var reports = task.Result;
                return reports.Select(report => new ReportResponseDto
                {
                    Id = report.IdReport,
                    Name = report.Reason, // Using Reason as Name (mock)
                    Lastname = report.Details, // Using Details as Lastname (mock)
                    ReportDate = report.ReportDate,
                    IsResolved = report.Status == "Resolved"
                }).ToList();
            });
    }

    public Task<bool> ResolveReportAsync(long reportId)
    {
        // TODO: Mark report as resolved
    return _reportRepository.ResolveReportAsync(reportId);
    }

    public Task<bool> DeleteReportAsync(long reportId)
    {
        // TODO: Delete report from database
    return _reportRepository.DeleteReportAsync(reportId);
    }
}
