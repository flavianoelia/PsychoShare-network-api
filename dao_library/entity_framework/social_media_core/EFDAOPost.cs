
using dao_library.Contexts;

public class EFDAOPost : DAOPost
{
    private AppDbContext dbContext;
    public EFDAOPost(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public Post? GetPost(long id)
    {
        return dbContext.Posts.FirstOrDefault(p => p.Id == id);
    }
    public List<Post> GetPostFromUser(long IdUser)
    {
        return dbContext.Posts.Where(p => p.UserId == IdUser).ToList();
    }
    public List<Post> GetAllPosts()
    {
        return this.dbContext.Posts.ToList();
    }
    public void Save(Post post)
    {
        dbContext.Posts.Add(post);
        dbContext.SaveChanges();
    }
    public void UpdatePost(long IdPost)
    {
        var existingPost = dbContext.Posts.FirstOrDefault(p => p.Id == IdPost);
        if (existingPost != null)
        {
            dbContext.Posts.Update(existingPost);
            dbContext.SaveChanges();
        }
    }
    public void Delete(long IdPost)
    {
        var existingPost = dbContext.Posts.FirstOrDefault(p => p.Id == IdPost);
        if (existingPost != null)
        {
            dbContext.Posts.Remove(existingPost);
            dbContext.SaveChanges();
        }
    }
    public async Task SaveAsync(Post post)
    {
        await dbContext.Posts.AddAsync(post);
        await dbContext.SaveChangesAsync();
    }
}