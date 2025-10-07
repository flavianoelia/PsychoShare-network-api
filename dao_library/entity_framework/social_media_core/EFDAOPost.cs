
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
        throw new NotImplementedException();
    }
    public List<Post> GetPostFromUser(long IdUser)
    {
        throw new NotImplementedException();
    }
    public List<Post> GetAllPosts()
    {
        throw new NotImplementedException();
    }
    public void Save(Post post)
    {
        throw new NotImplementedException();
    }
    public void UpdatePost(long IdPost)
    {
        throw new NotImplementedException();
    }
    public async Task SaveAsync(Post post)
    {
        await dbContext.Posts.AddAsync(post);
        await dbContext.SaveChangesAsync();
    }
}