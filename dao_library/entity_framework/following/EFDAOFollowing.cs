using dao_library.Contexts;
using dao_library.interfaces.following;
using entity_library.following;
using entity_library.system;
using Microsoft.EntityFrameworkCore;

namespace dao_library.entity_framework.following;

public class EFDAOFollowing : DAOFollowing
{   
    private readonly AppDbContext _dbContext;
    
    public EFDAOFollowing(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public List<User> GetContactsFromUser(long userId)
    {
        return _dbContext.Followings
            .Where(f => f.UserId == userId)
            .Include(f => f.FollowedUser)
            .Select(f => f.FollowedUser!)
            .ToList();
    }

    public void Save(Following following)
    {
        _dbContext.Followings.Add(following);
        _dbContext.SaveChanges();
    }
    
    public void Delete(Following following)
    {
        _dbContext.Followings.Remove(following);
        _dbContext.SaveChanges();
    }
    
    public List<User> GetFollowersFromUser(long userId)
    {
        return _dbContext.Followings
            .Where(f => f.FollowedId == userId)
            .Include(f => f.User)
            .Select(f => f.User!)
            .ToList();
    }
    
    public bool CheckFollowing(long userId, long followedUserId)
    {
        return _dbContext.Followings
            .Any(f => f.UserId == userId && f.FollowedId == followedUserId);
    }
    
    public bool DeleteByUserIds(long userId, long followedUserId)
    {
        var following = _dbContext.Followings
            .FirstOrDefault(f => f.UserId == userId && f.FollowedId == followedUserId);
            
        if (following == null)
            return false;
            
        _dbContext.Followings.Remove(following);
        _dbContext.SaveChanges();
        return true;
    }
    
    public List<long> GetFollowingIds(long userId)
    {
        return _dbContext.Followings
            .Where(f => f.UserId == userId)
            .Select(f => f.FollowedId)
            .ToList();
    }
    
    public Dictionary<long, bool> CheckMultipleFollowing(long userId, List<long> targetUserIds)
    {
        var followedIds = _dbContext.Followings
            .Where(f => f.UserId == userId && targetUserIds.Contains(f.FollowedId))
            .Select(f => f.FollowedId)
            .ToHashSet();
        
        return targetUserIds.ToDictionary(
            targetId => targetId,
            targetId => followedIds.Contains(targetId)
        );
    }
}