using Microsoft.EntityFrameworkCore;
namespace dao_library.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Ban> Bans { get; set; }
    public DbSet<Following> Followings { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Pdf> Pdfs { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    
}
