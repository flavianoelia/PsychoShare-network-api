public class FollowingRepository : IFollowingRepository
{
    public Task<Following> CreateFollowingAsync(Following following)
    {
        // TODO: Implement create following logic
        throw new NotImplementedException();
    }

    public Task<bool> DeleteFollowingAsync(long userId, long followedId)
    {
        // TODO: Implement delete following logic
        throw new NotImplementedException();
    }

    public Task<Following?> GetFollowingAsync(long userId, long followedId)
    {
        // TODO: Implement get following logic
        throw new NotImplementedException();
    }

    public Task<List<Following>> GetFollowersAsync(long userId)
    {
        // TODO: Implement get followers logic
        throw new NotImplementedException();
    }

    public Task<List<Following>> GetFollowingListAsync(long userId)
    {
        // TODO: Implement get user following logic
        throw new NotImplementedException();
    }

    public Task<bool> CheckIsFollowingAsync(long userId, long followedId)
    {
        // TODO: Implement check is following logic
        throw new NotImplementedException();
    }

    public Task<int> GetFollowersCountAsync(long userId)
    {
        // TODO: Implement get followers count logic
        throw new NotImplementedException();
    }

    public Task<int> GetFollowingCountAsync(long userId)
    {
        // TODO: Implement get following count logic
        throw new NotImplementedException();
    }
}
