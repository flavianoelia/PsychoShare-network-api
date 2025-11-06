using dao_library.Contexts;
using entity_library.system;
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
        this.dbContext.Users.Add(user);
        this.dbContext.SaveChanges();
    }
    
    public void UpdateUser(long idUser)
    {
        var user = dbContext.Users
            //.Include(u => u.Image) // solo si usas imagen
            .Include(u => u.Role)  // solo si usas role
            .FirstOrDefault(u => u.Id == idUser);

        if (user == null)
            throw new Exception("Usuario no encontrado.");

        dbContext.Users.Update(user);
        dbContext.SaveChanges();
    }
    public bool ExistsByEmailExceptUser(string email, long userId)
    {
        return dbContext.Users.Any(u => u.Email == email && u.Id != userId);
    }
    
    public void Delete(long IdUser)
    {
        var user = this.dbContext.Users.Find(IdUser);
        if (user != null)
        {
            this.dbContext.Users.Remove(user);
            this.dbContext.SaveChanges();
        }
    }
    public async Task SaveAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }
}
