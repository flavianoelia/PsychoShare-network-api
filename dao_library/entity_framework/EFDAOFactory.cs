public class EFDAOFactory : DAOFactory
{
    public DAOUser CreateDAOUser()
    {
        return new EFDAOUser();
    }
}