public class EFDAOFactory : DAOFactory
{
    public DAOComment DAOComment()
    {
        throw new NotImplementedException();
    }

    // Eliminado: public DAOFollowing DAOFollowing()

    public DAOPost DaoPost()
    {
        throw new NotImplementedException();
    }

    public DAOUser DAOUser()
    {
        return new EFDAOUser();
    }
}