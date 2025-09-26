using entity_library.system;

public interface DAOUser
{
    User? GetUser(long idUser);
    User? GetUserByEmail(string email);

    void Save(User user);

    void UpdateUser(long IdUser);
    void Delete(long IdUser);
    Task SaveAsync(User user);
}