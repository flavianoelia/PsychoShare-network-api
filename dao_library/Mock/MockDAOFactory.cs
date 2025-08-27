public class MockDAOFactory : DAOFactory
{
    public DAOUser CreateDAOUser()
    {
        return new MockDAOUser();
    }
}