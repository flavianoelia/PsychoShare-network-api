namespace psychoshare_api.Services;

public class FollowingService : IFollowingService
{
    private readonly IFollowingRepository _followingRepository;

    public FollowingService(IFollowingRepository followingRepository)
    {
        _followingRepository = followingRepository;
    }

    public async Task<Following> CreateFollowingAsync(Following following)
    {
    return await _followingRepository.CreateFollowingAsync(following);
    }

    public Task<bool> DeleteFollowingAsync(long userId, long followedId)
    {
    return _followingRepository.DeleteFollowingAsync(userId, followedId);
    }

    public Task<List<Following>> GetFollowersAsync(long userId)
    {
    return _followingRepository.GetFollowersAsync(userId);
    }

    public Task<List<Following>> GetFollowingAsync(long userId)
    {
    return _followingRepository.GetFollowingListAsync(userId);
    }

    public Task<bool> CheckIsFollowingAsync(long userId, long followedId)
    {
    return _followingRepository.CheckIsFollowingAsync(userId, followedId);
    }

    public Task<int> GetFollowersCountAsync(long userId)
    {
        // TODO: Add business logic validation
        return _followingRepository.GetFollowersCountAsync(userId);
    }

    public Task<int> GetFollowingCountAsync(long userId)
    {
        // TODO: Add business logic validation
        return _followingRepository.GetFollowingCountAsync(userId);
    }
}
