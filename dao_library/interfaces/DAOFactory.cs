public interface DAOFactory
{
    DAOUser DAOUser();
    DAOPost DaoPost();
    DAOComment DAOComment();
    DAOFollowing DAOFollowing();
}   