using dao_library.interfaces.admin;
using dao_library.interfaces.social_media_core;
using dao_library.interfaces.following;
using dao_library.interfaces.media;

public interface DAOFactory
{
    DAOUser DAOUser();
    DAOPost DaoPost();
    DAOComment DAOComment();
    DAOLike DAOLike();
    DAOFollowing DAOFollowing();
    DAOReport DAOReport();
    DAOBan DAOBan();
    DAOAvatar DAOAvatar();
}   