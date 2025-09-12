public class EFDAOFactory : DAOFactory
{
    public DAOComment DAOComment()
    {
        throw new NotImplementedException();
    }

    public DAOFollowing DAOFollowing()
    {
        throw new NotImplementedException();
    }

    public DAOPost DaoPost()
    {
        throw new NotImplementedException();
    }

    public DAOUser DAOUser()
    {
        return new EFDAOUser();
    }
}