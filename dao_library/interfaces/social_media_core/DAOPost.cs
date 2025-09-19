public interface DAOPost
{
    Post? GetPost(long id);
    List<Post> GetPostFromUser(long IdUser);
    List<Post> GetAllPosts();
    void Save(Post post);
    void UpdatePost(long IdPost);
}