public interface IReportRepository
{
    Task<int> CreateReportAsync(Report report);
    Task<Report?> GetReportByIdAsync(int reportId);
    Task<List<Report>> GetAllReportsAsync();
    Task<bool> UpdateReportAsync(Report report);
    Task<bool> DeleteReportAsync(int reportId);
    Task<bool> ResolveReportAsync(int reportId);
}

public class ReportRepository : IReportRepository
{
    public Task<int> CreateReportAsync(Report report)
    {
        // TODO: Implement database creation
        throw new NotImplementedException();
    }

    public Task<Report?> GetReportByIdAsync(int reportId)
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

    public Task<bool> DeleteReportAsync(int reportId)
    {
        // TODO: Implement database deletion
        throw new NotImplementedException();
    }

    public Task<bool> ResolveReportAsync(int reportId)
    {
        // TODO: Mark report as resolved
        throw new NotImplementedException();
    }
}
