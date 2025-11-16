using entity_library.media;
using entity_library.system;

public interface DAOAvatar
{
    public Avatar? GetAvatarByUserId(long idUser);
    public void Save(Avatar avatar);
    public void UpdateAvatar(long idUser);
    public void Delete(long idUser);
}