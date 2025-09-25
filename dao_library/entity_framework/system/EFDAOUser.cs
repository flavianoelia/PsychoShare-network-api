using dao_library.Contexts;
using Microsoft.EntityFrameworkCore;

public class EFDAOUser : DAOUser
{
    private AppDbContext dbContext; 
    public EFDAOUser(AppDbContext dbContext) 
    {
        this.dbContext = dbContext;
    }
    public User? GetUser(long id)
    {
        User? user = this.dbContext.Users.Where(user => user.Id == id).FirstOrDefault(); 
        return user;
    }
    public User? GetUserByEmail(string email)
    {
        User? user = this.dbContext.Users.Where(user => user.Email == email).FirstOrDefault();
        return user;
    }

    public void Save(User user)
    {
        throw new NotImplementedException();
    }
    public void UpdateUser(long idUser)
    {
        throw new NotImplementedException();
    }
    public void Delete(long IdUser)
    {
        throw new NotImplementedException();
    }
}
