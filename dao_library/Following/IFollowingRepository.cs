public interface IFollowingRepository
{
    Task<Following> CreateFollowingAsync(Following following);
    Task<bool> DeleteFollowingAsync(long userId, long followedId);
    Task<Following?> GetFollowingAsync(long userId, long followedId);
    Task<List<Following>> GetFollowersAsync(long userId);
    Task<List<Following>> GetFollowingListAsync(long userId);
    Task<bool> CheckIsFollowingAsync(long userId, long followedId);
    Task<int> GetFollowersCountAsync(long userId);
    Task<int> GetFollowingCountAsync(long userId);
}
