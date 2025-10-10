
using dao_library.Contexts;
using dao_library.interfaces.social_media_core;
using Microsoft.EntityFrameworkCore;

public class EFDAOComment : DAOComment
{   
    private readonly AppDbContext _dbContext;
    
    public EFDAOComment(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Get initial comments (first 2 for UX) - ordered by newest first
    public List<Comment> getCommentsFromPost(long IdPost, int limit = 2)
    {
        return _dbContext.Comments
            .Include(c => c.User)
            .Where(c => c.PostId == IdPost)
            .OrderByDescending(c => c.Id)
            .Take(limit)
            .ToList();
    }

    // Get paginated comments for "see more" functionality
    public List<Comment> getCommentsFromPostPaged(long IdPost, int skip, int take)
    {
        return _dbContext.Comments
            .Include(c => c.User)
            .Where(c => c.PostId == IdPost)
            .OrderByDescending(c => c.Id)
            .Skip(skip)
            .Take(take)
            .ToList();
    }

    // Get total count for pagination calculations
    public int GetCommentsCount(long IdPost)
    {
        return _dbContext.Comments
            .Where(c => c.PostId == IdPost)
            .Count();
    }

    // Save new comment
    public void Save(Comment comment)
    {
        _dbContext.Comments.Add(comment);
        _dbContext.SaveChanges();
    }

    // Delete comment by ID
    public void Delete(long IdComment)
    {
        var comment = _dbContext.Comments.Find(IdComment);
        if (comment != null)
        {
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
        }
    }

    // Get comment by ID
    public Comment? GetById(long id)
    {
        return _dbContext.Comments
            .Include(c => c.User)
            .FirstOrDefault(c => c.Id == id);
    }

    // Update comment text
    public void Update(long id, string newText)
    {
        var comment = _dbContext.Comments.Find(id);
        if (comment != null)
        {
            comment.Text = newText;
            _dbContext.SaveChanges();
        }
    }
}