public interface DAOUser
{
    User? GetUser(long id);
    void Save(User user);
}