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
            .Include(u => u.Image) // solo si usas imagen
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
    public (List<User> Users, int TotalCount) GetAllPaginated(int page, int size, string? search = null, string? role = null)
    {
        var query = dbContext.Users
            .Include(u => u.Role)
            .Include(u => u.Image)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchLower = search.Trim().ToLower();
            query = query.Where(u => 
                u.Name.ToLower().Contains(searchLower) ||
                u.LastName.ToLower().Contains(searchLower) ||
                u.Email.ToLower().Contains(searchLower));
        }

        if (!string.IsNullOrWhiteSpace(role))
        {
            query = query.Where(u => u.Role != null && u.Role.RoleName == role);
        }

        var totalCount = query.Count();

        var users = query
            .OrderBy(u => u.Id)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();

        return (users, totalCount);
    }

    public async Task SaveAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
        await dbContext.SaveChangesAsync();
    }
}
