using dao_library.Contexts;
using dao_library.interfaces.admin;
using entity_library.ReportPolicy;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.admin;

public class EFDAOReport : DAOReport
{
    private readonly AppDbContext _dbContext;

    public EFDAOReport(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Save(Report report)
    {
        _dbContext.Reports.Add(report);
        _dbContext.SaveChanges();
    }

    public List<Report> GetAll()
    {
        return _dbContext.Reports
            .Include(r => r.ReporterUser)
            .Include(r => r.ReportedUser)
            .ToList();
    }

    public (List<Report> Reports, int TotalCount) GetAllPaginated(int page, int size, string? status = null, string? contentType = null, DateTime? dateFrom = null, DateTime? dateTo = null)
    {
        var query = _dbContext.Reports
            .Include(r => r.ReporterUser)
            .Include(r => r.ReportedUser)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(status))
        {
            query = query.Where(r => r.Status == status.Trim());
        }

        if (!string.IsNullOrWhiteSpace(contentType))
        {
            query = query.Where(r => r.ContentType == contentType.Trim());
        }

        if (dateFrom.HasValue)
        {
            query = query.Where(r => r.ReportDate >= dateFrom.Value);
        }

        if (dateTo.HasValue)
        {
            query = query.Where(r => r.ReportDate <= dateTo.Value);
        }

        var totalCount = query.Count();

        var reports = query
            .OrderByDescending(r => r.ReportDate)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();

        return (reports, totalCount);
    }

    public Report? GetById(long id)
    {
        return _dbContext.Reports
            .Include(r => r.ReporterUser)
            .Include(r => r.ReportedUser)
            .FirstOrDefault(r => r.Id == id);
    }

    public void Update(Report report)
    {
        _dbContext.Reports.Update(report);
        _dbContext.SaveChanges();
    }

    public void Delete(Report report)
    {
        _dbContext.Reports.Remove(report);
        _dbContext.SaveChanges();
    }
}