public interface DAOFactory
{
    DAOUser DAOUser();
    DAOPost DaoPost();
    DAOComment DAOComment();
    // Eliminado: DAOFollowing DAOFollowing();
}   