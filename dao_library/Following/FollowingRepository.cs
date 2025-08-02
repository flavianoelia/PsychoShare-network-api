public class FollowingRepository : IFollowingRepository
{
    private static List<Following> _mockFollowings = new List<Following>();
    private static long _nextId = 1;

    public Task<Following> CreateFollowingAsync(Following following)
    {
        // Mock implementation: Create following relationship
        following.FollowingId = _nextId++;
        following.StartDate = DateTime.Now;
        
        _mockFollowings.Add(following);
        
        return Task.FromResult(following);
    }

    public Task<bool> DeleteFollowingAsync(long userId, long followedId)
    {
        // Mock implementation: Remove following relationship
        var following = _mockFollowings.FirstOrDefault(f => f.UserId == userId && f.FollowedId == followedId);
        if (following != null)
        {
            _mockFollowings.Remove(following);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<Following?> GetFollowingAsync(long userId, long followedId)
    {
        // Mock implementation: Get specific following relationship
        var following = _mockFollowings.FirstOrDefault(f => f.UserId == userId && f.FollowedId == followedId);
        return Task.FromResult(following);
    }

    public Task<List<Following>> GetFollowersAsync(long userId)
    {
        // Mock implementation: Get users following this user (userId is being followed)
        var followers = _mockFollowings.Where(f => f.FollowedId == userId).ToList();
        return Task.FromResult(followers);
    }

    public Task<List<Following>> GetFollowingListAsync(long userId)
    {
        // Mock implementation: Get users that this user follows
        var following = _mockFollowings.Where(f => f.UserId == userId).ToList();
        return Task.FromResult(following);
    }

    public Task<bool> CheckIsFollowingAsync(long userId, long followedId)
    {
        // Mock implementation: Check if user follows another user
        var exists = _mockFollowings.Any(f => f.UserId == userId && f.FollowedId == followedId);
        return Task.FromResult(exists);
    }

    public Task<int> GetFollowersCountAsync(long userId)
    {
        // Mock implementation: Count followers
        var count = _mockFollowings.Count(f => f.FollowedId == userId);
        return Task.FromResult(count);
    }

    public Task<int> GetFollowingCountAsync(long userId)
    {
        // Mock implementation: Count following
        var count = _mockFollowings.Count(f => f.UserId == userId);
        return Task.FromResult(count);
    }
}
