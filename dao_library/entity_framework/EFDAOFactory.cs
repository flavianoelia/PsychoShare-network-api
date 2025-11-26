using dao_library.Contexts;
using dao_library.interfaces.admin;
using dao_library.interfaces.social_media_core;
using dao_library.interfaces.following;
using dao_library.entity_framework.admin;
using dao_library.entity_framework.following;
using dao_library.entity_framework.media;
using dao_library.entity_framework.social_media_core;
namespace dao_library.interfaces.media;

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

    public DAOLike DAOLike()
    {
        return new EFDAOLike(this.appDbContext);
    }

    public DAOBan DAOBan()
    {
        return new EFDAOBan(this.appDbContext);
    }
    public DAOAvatar DAOAvatar()
    {
        return new EFDAOAvatar(this.appDbContext);
    }

    public IDAOImage DaoImage()
    {
        return new EFDAOImage(this.appDbContext);
    }
    public IDAOPdf DaoPdf()
    {
        return new EFDAOPdf(this.appDbContext);
    }
}
