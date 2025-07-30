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
    public Task<long> CreateBanAsync(Ban ban)
    {
        // TODO: Implement database creation
        throw new NotImplementedException();
    }

    public Task<Ban?> GetBanByIdAsync(long banId)
    {
        // TODO: Implement database retrieval
        throw new NotImplementedException();
    }

    public Task<Ban?> GetActiveBanByUserIdAsync(long userId)
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

    public Task<bool> DeleteBanAsync(long banId)
    {
        // TODO: Implement database deletion
        throw new NotImplementedException();
    }

    public Task<bool> CheckUserIsBannedAsync(long userId)
    {
        // TODO: Check if user has active ban
        throw new NotImplementedException();
    }
}
