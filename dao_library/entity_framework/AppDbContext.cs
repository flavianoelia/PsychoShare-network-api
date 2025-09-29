using Microsoft.EntityFrameworkCore;
using entity_library.system;
namespace dao_library.Contexts;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Unique index for Email in User y limitar longitud a 191 caracteres (MySQL utf8mb4)
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasMaxLength(191)
            .IsRequired();
    }
}
