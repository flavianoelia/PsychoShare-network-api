public class MockDAOFactory : DAOFactory
{
    public DAOUser CreateDAOUser()
    {
        return new MockDAOUser();
    }

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
        throw new NotImplementedException();
    }
}