using dao_library.Contexts;

public class EFDAOFactory : DAOFactory
{
    private AppDbContext appDbContext;
    public EFDAOFactory(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public DAOComment DAOComment()
    {
        return new EFDAOComment(this.appDbContext);
    }

    public DAOFollowing DAOFollowing()
    {
        return new EFDAOFollowing(this.appDbContext);
    }

    public DAOPost DaoPost()
    {
        return new EFDAOPost(this.appDbContext);
    }

    public DAOUser DAOUser()
    {
        return new EFDAOUser(this.appDbContext); //agregado
    }
}
