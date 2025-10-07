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