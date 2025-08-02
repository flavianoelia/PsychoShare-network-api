public interface IBanRepository
{
    Task<long> CreateBanAsync(Ban ban);
    Task<Ban?> GetBanByIdAsync(long banId);
    Task<Ban?> GetActiveBanByUserIdAsync(long userId);
    Task<List<Ban>> GetAllActiveBansAsync();
    Task<bool> UpdateBanAsync(Ban ban);
    Task<bool> DeleteBanAsync(long banId);
    Task<bool> CheckUserIsBannedAsync(long userId);
}

public class BanRepository : IBanRepository
{
    private static List<Ban> _mockBans = new List<Ban>();
    private static long _nextId = 1;

    public Task<long> CreateBanAsync(Ban ban)
    {
        // Mock implementation: Create ban
        ban.IdBan = _nextId++;
        ban.StartDate = DateTime.Now;
        ban.IsActive = true;
        
        _mockBans.Add(ban);
        
        return Task.FromResult(ban.IdBan);
    }

    public Task<Ban?> GetBanByIdAsync(long banId)
    {
        // Mock implementation: Get ban by ID
        var ban = _mockBans.FirstOrDefault(b => b.IdBan == banId);
        return Task.FromResult(ban);
    }

    public Task<Ban?> GetActiveBanByUserIdAsync(long userId)
    {
        // Mock implementation: Get active ban for user
        var activeBan = _mockBans.FirstOrDefault(b => b.BannedUserId == userId && 
            b.IsActive && (b.EndDate == null || b.EndDate > DateTime.Now));
        return Task.FromResult(activeBan);
    }

    public Task<List<Ban>> GetAllActiveBansAsync()
    {
        // Mock implementation: Get all active bans
        var activeBans = _mockBans.Where(b => b.IsActive && 
            (b.EndDate == null || b.EndDate > DateTime.Now)).ToList();
        return Task.FromResult(activeBans);
    }

    public Task<bool> UpdateBanAsync(Ban ban)
    {
        // Mock implementation: Update ban
        var existingBan = _mockBans.FirstOrDefault(b => b.IdBan == ban.IdBan);
        if (existingBan != null)
        {
            existingBan.Reason = ban.Reason;
            existingBan.EndDate = ban.EndDate;
            existingBan.BanType = ban.BanType;
            existingBan.IsActive = ban.IsActive;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> DeleteBanAsync(long banId)
    {
        // Mock implementation: Delete ban (mark as inactive)
        var ban = _mockBans.FirstOrDefault(b => b.IdBan == banId);
        if (ban != null)
        {
            ban.IsActive = false; // Soft delete
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> CheckUserIsBannedAsync(long userId)
    {
        // Mock implementation: Check if user is currently banned
        var activeBan = _mockBans.Any(b => b.BannedUserId == userId && 
            b.IsActive && (b.EndDate == null || b.EndDate > DateTime.Now));
        return Task.FromResult(activeBan);
    }
}
