using entity_library.media;
namespace dao_library.interfaces.media;
public interface DAOAvatar
{
    public Avatar? GetAvatarByUserId(long userId);
    public void Save(Avatar avatar);
    public void UpdateAvatarByUserId(long userId, Avatar avatar);
    public void DeleteByUserId(long userId);
    
}
