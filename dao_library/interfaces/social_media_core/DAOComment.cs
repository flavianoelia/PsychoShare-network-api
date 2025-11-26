public interface DAOComment
{
    // Get initial comments (first 2 for UX)
    public List<Comment> getCommentsFromPost(long IdPost, int limit = 2);
    
    // Get paginated comments for "see more" functionality  
    public List<Comment> getCommentsFromPostPaged(long IdPost, int skip, int take);
    
    // Get total count for pagination calculations
    public int GetCommentsCount(long IdPost);
    
    // CRUD operations
    public void Save(Comment comment);
    public void Delete(long IdComment);
    public Comment? GetById(long id);
    public void Update(long id, string newText);
}