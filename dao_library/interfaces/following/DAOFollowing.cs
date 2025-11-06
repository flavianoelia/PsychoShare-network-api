using entity_library.following;
using entity_library.system;

namespace dao_library.interfaces.following;

public interface DAOFollowing
{
    public void Save(Following following);
    
    public void Delete(Following following);

    public List<User> GetContactsFromUser(long userId);
    
    public List<User> GetFollowersFromUser(long userId);
    
    public bool CheckFollowing(long userId, long followedId);
    
    public bool DeleteByUserIds(long userId, long followedUserId);
}