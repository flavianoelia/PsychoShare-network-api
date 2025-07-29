public interface IBanRepository
{
    Task<int> CreateBanAsync(Ban ban);
    Task<Ban?> GetBanByIdAsync(int banId);
    Task<Ban?> GetActiveBanByUserIdAsync(int userId);
    Task<List<Ban>> GetAllActiveBansAsync();
    Task<bool> UpdateBanAsync(Ban ban);
    Task<bool> DeleteBanAsync(int banId);
    Task<bool> CheckUserIsBannedAsync(int userId);
}

public class BanRepository : IBanRepository
{
    public Task<int> CreateBanAsync(Ban ban)
    {
        // TODO: Implement database creation
        throw new NotImplementedException();
    }

    public Task<Ban?> GetBanByIdAsync(int banId)
    {
        // TODO: Implement database retrieval
        throw new NotImplementedException();
    }

    public Task<Ban?> GetActiveBanByUserIdAsync(int userId)
    {
        // TODO: Implement get active ban for user
        throw new NotImplementedException();
    }

    public Task<List<Ban>> GetAllActiveBansAsync()
    {
        // TODO: Implement get all active bans
        throw new NotImplementedException();
    }

    public Task<bool> UpdateBanAsync(Ban ban)
    {
        // TODO: Implement database update
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBanAsync(int banId)
    {
        // TODO: Implement database deletion
        throw new NotImplementedException();
    }

    public Task<bool> CheckUserIsBannedAsync(int userId)
    {
        // TODO: Check if user has active ban
        throw new NotImplementedException();
    }
}
