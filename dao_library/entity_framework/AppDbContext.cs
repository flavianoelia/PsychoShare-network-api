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

        // Ignore User.Avatar navigation property to prevent bidirectional relationship inference
        // Avatar relationship is handled via Image→User (Avatar inherits from Image)
        modelBuilder.Entity<User>()
            .Ignore(u => u.Avatar);

        // Configure Comment foreign keys with CASCADE delete
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Post)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure Following relationships
        modelBuilder.Entity<Following>()
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Following>()
            .HasOne(f => f.FollowedUser)
            .WithMany()
            .HasForeignKey(f => f.FollowedId)
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

        // Configure Image (base class for Avatar and Image) with CASCADE delete
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
