using dao_library.Contexts;
using dao_library.interfaces.social_media_core;
using entity_library.system;
using Microsoft.EntityFrameworkCore;

public class EFDAOLike : DAOLike
{
    private readonly AppDbContext _dbContext;

    public EFDAOLike(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    
    public bool ToggleLike(long userId, long postId)
    {
        
        var existingLike = _dbContext.Likes
            .FirstOrDefault(l => l.UserId == userId && l.PostId == postId);

        if (existingLike != null)
        {
            // Si existe, eliminarlo (unlike)
            _dbContext.Likes.Remove(existingLike);
            _dbContext.SaveChanges();
            return false; 
        }
        else
        {
            // Si no existe, crearlo (like)
            var newLike = new Like
            {
                UserId = userId,
                PostId = postId
            };
            _dbContext.Likes.Add(newLike);
            _dbContext.SaveChanges();
            return true; 
        }
    }

    
    public List<Like> GetPostLikes(long postId)
    {
        return _dbContext.Likes
            .Include(l => l.User)
            .Where(l => l.PostId == postId)
            .OrderByDescending(l => l.Id) 
            .ToList();
    }

    
    public List<Like> GetUserLikes(long userId)
    {
        return _dbContext.Likes
            .Include(l => l.Post)
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.Id) 
            .ToList();
    }

    
    public (int likeCount, bool isLikedByCurrentUser) GetLikeStats(long postId, long currentUserId)
    {
        var likeCount = _dbContext.Likes.Count(l => l.PostId == postId);
        var isLikedByCurrentUser = _dbContext.Likes
            .Any(l => l.PostId == postId && l.UserId == currentUserId);

        return (likeCount, isLikedByCurrentUser);
    }

    // Helper: Verificar si un usuario ya dio like a un post
    public bool IsLiked(long userId, long postId)
    {
        return _dbContext.Likes.Any(l => l.UserId == userId && l.PostId == postId);
    }

    public void Save(Like like)
    {
        
        var existingLike = _dbContext.Likes
            .FirstOrDefault(l => l.UserId == like.UserId && l.PostId == like.PostId);

        if (existingLike == null)
        {
            _dbContext.Likes.Add(like);
            _dbContext.SaveChanges();
        }
        // Si ya existe, no hacer nada (prevenir duplicados)
    }

    
    public void Delete(Like like)
    {
        _dbContext.Likes.Remove(like);
        _dbContext.SaveChanges();
    }

    
    public Like? GetById(long id)
    {
        return _dbContext.Likes
            .Include(l => l.User)
            .Include(l => l.Post)
            .FirstOrDefault(l => l.Id == id);
    }

    
    public List<Like> GetAll()
    {
        return _dbContext.Likes
            .Include(l => l.User)
            .Include(l => l.Post)
            .OrderByDescending(l => l.Id)
            .ToList();
    }
}