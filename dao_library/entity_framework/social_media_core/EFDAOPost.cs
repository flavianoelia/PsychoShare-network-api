
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

    public (List<Post> Posts, int TotalCount) GetAllPostsPaginated(int page, int size, string? searchTerm = null)
    {
        var query = dbContext.Posts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.Trim().ToLower();
            query = query.Where(p => 
                p.Title.ToLower().Contains(search) || 
                p.Description.ToLower().Contains(search) ||
                p.Authorship.ToLower().Contains(search));
        }

        var totalCount = query.Count();

        var posts = query
            .OrderByDescending(p => p.Id)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();

        return (posts, totalCount);
    }

    public (List<Post> Posts, int TotalCount) GetFeedPosts(long currentUserId, int page, int size, string? searchTerm = null)
    {
        var followedUserIds = dbContext.Followings
            .Where(f => f.UserId == currentUserId)
            .Select(f => f.FollowedId)
            .ToList();

        var query = dbContext.Posts.AsQueryable()
            .Where(p => p.UserId == currentUserId || followedUserIds.Contains(p.UserId));

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var search = searchTerm.Trim().ToLower();
            query = query.Where(p => 
                p.Title.ToLower().Contains(search) || 
                p.Description.ToLower().Contains(search) ||
                p.Authorship.ToLower().Contains(search));
        }

        var totalCount = query.Count();

        var posts = query
            .OrderByDescending(p => p.Id)
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();

        return (posts, totalCount);
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