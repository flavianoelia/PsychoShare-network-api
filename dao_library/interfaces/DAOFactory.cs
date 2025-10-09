using dao_library.interfaces.admin;
using dao_library.interfaces.social_media_core;

public interface DAOFactory
{
    DAOUser DAOUser();
    DAOPost DaoPost();
    DAOComment DAOComment();
    DAOLike DAOLike();
    DAOReport DAOReport();
    DAOBan DAOBan();
}   