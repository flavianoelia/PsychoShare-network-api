using Microsoft.EntityFrameworkCore;
using entity_library.system;
using entity_library.ReportPolicy;
using entity_library.following;
namespace dao_library.Contexts;
using entity_library.media;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Ban> Bans { get; set; }
    public DbSet<Following> Followings { get; set; }
    public DbSet<BaseFile> BaseFiles { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Avatar> Avatar{ get; set; }
    public DbSet<Pdf> Pdfs { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<User>()
        .Property(u => u.RoleType)
        .HasConversion<int>(); // save enum like int

        base.OnModelCreating(modelBuilder);

        // Configure TPH inheritance with discriminator
        modelBuilder.Entity<Person>()
            .HasDiscriminator<string>("PersonType")
            .HasValue<Person>("Person")
            .HasValue<User>("User");

        // Configure User entity constraints
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasMaxLength(191)
            .IsRequired();

        // NOTE: Keep only approved cascade configurations.
        // Comment and Following cascade configurations were removed to avoid
        // conflicting behaviors with the TPH File/Avatar model.
        
        // Configure User→Avatar relationship with CASCADE delete
        modelBuilder.Entity<User>()
            .HasOne(u => u.Avatar)
            .WithOne(a => a.User)       // relación avatar-user
            .HasForeignKey<Avatar>(a => a.IdUser)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure User→Posts relationship with CASCADE delete
        modelBuilder.Entity<Post>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure User→Likes relationship with CASCADE delete
        modelBuilder.Entity<Like>()
            .HasOne(l => l.User)
            .WithMany()
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Post→Likes relationship with CASCADE delete
        modelBuilder.Entity<Like>()
            .HasOne(l => l.Post)
            .WithMany(p => p.Likes)
            .HasForeignKey(l => l.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Image (includes Avatar) relationship with CASCADE delete
        modelBuilder.Entity<Image>()
            .HasOne(i => i.User)
            .WithMany()
            .HasForeignKey(i => i.IdUser)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Pdf relationship with CASCADE delete
        modelBuilder.Entity<Pdf>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.IdUser)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
