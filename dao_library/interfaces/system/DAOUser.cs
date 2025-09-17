public interface DAOUser
{
    User? GetUser(long idUser);
    void Save(User user);

    void UpdateUser(long IdUser);
    void Delete(long IdUser);
}