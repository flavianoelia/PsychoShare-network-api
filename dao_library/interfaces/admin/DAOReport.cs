using entity_library.ReportPolicy;

namespace dao_library.interfaces.admin;

public interface DAOReport
{
    public void Save(Report report);
    public List<Report> GetAll();
    public Report? GetById(long id);
    public void Update(Report report);
    public void Delete(Report report);
}