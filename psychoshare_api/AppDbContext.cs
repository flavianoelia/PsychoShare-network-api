using Microsoft.EntityFrameworkCore;

namespace psychoshare_api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Ejemplo de DbSet para una entidad User
        // public DbSet<User> Users { get; set; }
        // Agrega aquí tus DbSet para cada entidad que quieras mapear a la base de datos
    }
}
