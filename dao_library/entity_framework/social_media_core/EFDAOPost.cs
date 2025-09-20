
using dao_library.Contexts;

public class EFDAOPost : DAOPost
{   
    private AppDbContext dbContext; //agregado
    public EFDAOPost(AppDbContext dbContext) //agregado
    {
        this.dbContext = dbContext;//agregado
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
}