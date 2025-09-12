public interface DAOPost
{
    Post? GetPost(long id);
    
    List<Post> GetAllPosts();
}