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
        // TODO: Validate report data
        // TODO: Convert DTO to entity
        // TODO: Save to database
        // TODO: Return response DTO
        throw new NotImplementedException();
    }

    public Task<ReportResponseDto?> GetReportByIdAsync(long reportId)
    {
        // TODO: Get report from repository
        // TODO: Convert entity to DTO
        throw new NotImplementedException();
    }

    public Task<List<ReportResponseDto>> GetAllReportsAsync()
    {
        // TODO: Get all reports
        // TODO: Convert entities to DTOs
        throw new NotImplementedException();
    }

    public Task<bool> ResolveReportAsync(long reportId)
    {
        // TODO: Mark report as resolved
        throw new NotImplementedException();
    }

    public Task<bool> DeleteReportAsync(long reportId)
    {
        // TODO: Delete report from database
        throw new NotImplementedException();
    }
}
