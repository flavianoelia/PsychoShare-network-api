using dao_library.interfaces.admin;
using dao_library.interfaces.social_media_core;
using dao_library.interfaces.following;

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

    public DAOReport DAOReport()
    {
        throw new NotImplementedException();
    }

    public DAOLike DAOLike()
    {
        throw new NotImplementedException();
    }

    public DAOBan DAOBan()
    {
        throw new NotImplementedException();
    }
}