using entity_library.system;

public interface DAOUser
{
    User? GetUser(long idUser);
    User? GetUserByEmail(string email);
    (List<User> Users, int TotalCount) GetAllPaginated(int page, int size, string? search = null, string? role = null);

    void Save(User user);

    void UpdateUser(long IdUser);
    void Delete(long IdUser);
    Task SaveAsync(User user);
}