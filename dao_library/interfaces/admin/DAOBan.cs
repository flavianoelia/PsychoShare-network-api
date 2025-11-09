using entity_library.ReportPolicy;

namespace dao_library.interfaces.admin;

public interface DAOBan
{
    
    public void Save(Ban ban);
    public Ban? GetById(long id);
    public void Update(Ban ban);
    public void Delete(Ban ban);
    
    
    public List<Ban> GetActiveBans();
    public (List<Ban> Bans, int TotalCount) GetActiveBansPaginated(int page, int size);
    public Ban? GetUserBan(long userId);
    public bool CheckBanStatus(long userId);
}