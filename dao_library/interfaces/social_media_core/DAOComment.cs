public interface DAOComment
{
    public List<Comment> getCommentsFromPost(long IdPost);
    public void Save(Comment comment);
    public void Delete(long IdComment);
}