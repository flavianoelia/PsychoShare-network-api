using dao_library.Contexts;
using dao_library.interfaces.admin;
using dao_library.entity_framework.admin;

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
        return new EFDAOUser(this.appDbContext);
    }

    public DAOReport DAOReport()
    {
        return new EFDAOReport(this.appDbContext);
    }

    public DAOBan DAOBan()
    {
        return new EFDAOBan(this.appDbContext);
    }
}
