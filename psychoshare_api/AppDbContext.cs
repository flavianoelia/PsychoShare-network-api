using entity_library.following;
using Microsoft.EntityFrameworkCore;

namespace psychoshare_api
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Following> Followings { get; set; }
    }
}
