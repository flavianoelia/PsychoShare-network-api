public interface DAOUser
{
    User? GetUser(long idUser);
    User? GetUserByEmail(string email);
    User? GetUserByUsername(string username);
    void Save(User user);

    void UpdateUser(long IdUser);
    void Delete(long IdUser);
}