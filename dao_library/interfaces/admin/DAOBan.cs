using entity_library.ReportPolicy;

namespace dao_library.interfaces.admin;

public interface DAOBan
{
    // Métodos básicos CRUD
    public void Save(Ban ban);
    public Ban? GetById(long id);
    public void Update(Ban ban);
    public void Delete(Ban ban);
    
    // Métodos específicos para bans
    public List<Ban> GetActiveBans();
    public Ban? GetUserBan(long userId);
    public bool CheckBanStatus(long userId);
}