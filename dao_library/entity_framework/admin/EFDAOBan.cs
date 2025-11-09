using dao_library.Contexts;
using dao_library.interfaces.admin;
using entity_library.ReportPolicy;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.admin;

public class EFDAOBan : DAOBan
{
    private readonly AppDbContext _dbContext;

    public EFDAOBan(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Save(Ban ban)
    {
        _dbContext.Bans.Add(ban);
        _dbContext.SaveChanges();
    }

    public Ban? GetById(long id)
    {
        return _dbContext.Bans
            .Include(b => b.BanUser)
            .FirstOrDefault(b => b.Id == id);
    }

    public void Update(Ban ban)
    {
        _dbContext.Bans.Update(ban);
        _dbContext.SaveChanges();
    }

    public void Delete(Ban ban)
    {
        _dbContext.Bans.Remove(ban);
        _dbContext.SaveChanges();
    }

    public List<Ban> GetActiveBans()
    {
        return _dbContext.Bans
            .Where(b => b.IsActive == true)
            .Include(b => b.BanUser)
            .ToList();
    }

    public Ban? GetUserBan(long userId)
    {
        return _dbContext.Bans
            .Where(b => b.BannedUserId == userId)
            .Include(b => b.BanUser)
            .OrderByDescending(b => b.StartDate)
            .FirstOrDefault();
    }

    public (List<Ban> Bans, int TotalCount) GetActiveBansPaginated(int page, int size)
    {
        var query = _dbContext.Bans
            .Where(b => b.IsActive == true)
            .Include(b => b.BanUser)
            .AsQueryable();

        var totalCount = query.Count();

        var bans = query
            .OrderByDescending(b => b.StartDate)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();

        return (bans, totalCount);
    }

    public bool CheckBanStatus(long userId)
    {
        return _dbContext.Bans
            .Any(b => b.BannedUserId == userId && b.IsActive == true);
    }
}