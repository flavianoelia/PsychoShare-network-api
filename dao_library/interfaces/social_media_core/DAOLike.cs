using entity_library.system;

namespace dao_library.interfaces.social_media_core;

public interface DAOLike
{
    
    public bool ToggleLike(long userId, long postId);
    
    
    public List<Like> GetPostLikes(long postId);
    public List<Like> GetUserLikes(long userId);
    
    
    public (int likeCount, bool isLikedByCurrentUser) GetLikeStats(long postId, long currentUserId);
    
    
    public bool IsLiked(long userId, long postId);
    
    
    public void Save(Like like);
    public void Delete(Like like);
    public Like? GetById(long id);
    public List<Like> GetAll();
}