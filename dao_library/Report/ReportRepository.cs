public interface IReportRepository
{
    Task<long> CreateReportAsync(Report report);
    Task<Report?> GetReportByIdAsync(long reportId);
    Task<List<Report>> GetAllReportsAsync();
    Task<bool> UpdateReportAsync(Report report);
    Task<bool> DeleteReportAsync(long reportId);
    Task<bool> ResolveReportAsync(long reportId);
}

public class ReportRepository : IReportRepository
{
    public Task<long> CreateReportAsync(Report report)
    {
        // TODO: Implement database creation
        throw new NotImplementedException();
    }

    public Task<Report?> GetReportByIdAsync(long reportId)
    {
        // TODO: Implement database retrieval
        throw new NotImplementedException();
    }

    public Task<List<Report>> GetAllReportsAsync()
    {
        // TODO: Implement get all reports
        throw new NotImplementedException();
    }

    public Task<bool> UpdateReportAsync(Report report)
    {
        // TODO: Implement database update
        throw new NotImplementedException();
    }

    public Task<bool> DeleteReportAsync(long reportId)
    {
        // TODO: Implement database deletion
        throw new NotImplementedException();
    }

    public Task<bool> ResolveReportAsync(long reportId)
    {
        // TODO: Mark report as resolved
        throw new NotImplementedException();
    }
}
