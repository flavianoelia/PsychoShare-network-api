namespace dao_library.Report
{
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
        private static List<Report> _mockReports = new List<Report>();
        private static long _nextId = 1;

        public Task<long> CreateReportAsync(Report report)
        {
            // Mock implementation: Create report
            report.IdReport = _nextId++;
            report.ReportDate = DateTime.Now;
        report.Status = "Pending"; // Default status
        
        _mockReports.Add(report);
        
        return Task.FromResult(report.IdReport);
    }

    public Task<Report?> GetReportByIdAsync(long reportId)
    {
        // Mock implementation: Get report by ID
        var report = _mockReports.FirstOrDefault(r => r.IdReport == reportId);
        return Task.FromResult(report);
    }

    public Task<List<Report>> GetAllReportsAsync()
    {
        // Mock implementation: Get all reports
        return Task.FromResult(_mockReports.ToList());
    }

    public Task<bool> UpdateReportAsync(Report report)
    {
        // Mock implementation: Update report
        var existingReport = _mockReports.FirstOrDefault(r => r.IdReport == report.IdReport);
        if (existingReport != null)
        {
            existingReport.Reason = report.Reason;
            existingReport.Details = report.Details;
            existingReport.Status = report.Status;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> DeleteReportAsync(long reportId)
    {
        // Mock implementation: Delete report
        var report = _mockReports.FirstOrDefault(r => r.IdReport == reportId);
        if (report != null)
        {
            _mockReports.Remove(report);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> ResolveReportAsync(long reportId)
    {
        // Mock implementation: Mark report as resolved
        var report = _mockReports.FirstOrDefault(r => r.IdReport == reportId);
        if (report != null)
        {
            report.Status = "Resolved";
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
        }
    }
}
