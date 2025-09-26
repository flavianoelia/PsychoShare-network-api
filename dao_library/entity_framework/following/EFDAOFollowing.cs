using dao_library.Contexts;
using entity_library.system;

public class EFDAOFollowing : DAOFollowing
{   
    private AppDbContext dbContext; //agregado
    public EFDAOFollowing(AppDbContext dbContext) //agregado
    {
        this.dbContext = dbContext;//agregado
    }
    public List<User> GetContactsFromUser(long userId)
    {
        throw new NotImplementedException();
    }

    public void Save(Following following)
    {
        throw new NotImplementedException();
    }
}