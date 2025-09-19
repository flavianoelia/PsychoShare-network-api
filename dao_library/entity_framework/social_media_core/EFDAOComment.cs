
public class EFDAOComment : DAOComment
{   
    private AppDbContext dbContext; //agregado
    public EFDAOComment(AppDbContext dbContext) //agregado
    {
        this.dbContext = dbContext;//agregado
    }
    public void Delete(long IdComment)
    {
        throw new NotImplementedException();
    }

    public List<Comment> getCommentsFromPost(long IdPost)
    {
        throw new NotImplementedException();
    }

    public void Save(Comment comment)
    {
        throw new NotImplementedException();
    }
}