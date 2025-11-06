public interface DAOPost
{
    Post? GetPost(long id);
    List<Post> GetPostFromUser(long IdUser);
    List<Post> GetAllPosts();
    (List<Post> Posts, int TotalCount) GetAllPostsPaginated(int page, int size, string? searchTerm = null);
    (List<Post> Posts, int TotalCount) GetFeedPosts(long currentUserId, int page, int size, string? searchTerm = null);
    void Save(Post post);
    void UpdatePost(long IdPost);
    void Delete(long IdPost);
}