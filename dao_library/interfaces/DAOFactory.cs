using dao_library.interfaces.admin;

public interface DAOFactory
{
    DAOUser DAOUser();
    DAOPost DaoPost();
    DAOComment DAOComment();
    DAOReport DAOReport();
    DAOBan DAOBan();
}   