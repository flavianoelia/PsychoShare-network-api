using Microsoft.EntityFrameworkCore;

public class EFDAOUser : DAOUser
{
    private AppDbContext dbContext; //agregado
    public EFDAOUser(AppDbContext dbContext) //agregado
    {
        this.dbContext = dbContext;//agregado
    }
    public User? GetUser(long id)
    {
        User? user = this.dbContext.Users.Where(user => user.Id == 1).FirstOrDefault(); //agregdo
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
