
using System.Reflection.Metadata;

public class MockDAOUser : DAOUser
{
    private static List<User> users = new List<User>();

    public MockDAOUser()
    {
    users.Add(new User { Name = "Pepe", LastName = "Roldan", Email = "pepe.com", PasswordHash = "12345", Id = 1 });
    users.Add(new User { Name = "Ana", LastName = "Gomez", Email = "ana.gomez@mail.com", PasswordHash = "pass123", Id = 2 });
    users.Add(new User { Name = "Luis", LastName = "Perez", Email = "luis.perez@mail.com", PasswordHash = "pass456", Id = 3 });
    users.Add(new User { Name = "Maria", LastName = "Lopez", Email = "maria.lopez@mail.com", PasswordHash = "pass789", Id = 4 });
    users.Add(new User { Name = "Carlos", LastName = "Martinez", Email = "carlos.m@mail.com", PasswordHash = "passabc", Id = 5 });
    users.Add(new User { Name = "Sofia", LastName = "Rodriguez", Email = "sofia.r@mail.com", PasswordHash = "passdef", Id = 6 });
    users.Add(new User { Name = "Javier", LastName = "Fernandez", Email = "javier.f@mail.com", PasswordHash = "passghi", Id = 7 });
    users.Add(new User { Name = "Laura", LastName = "Diaz", Email = "laura.d@mail.com", PasswordHash = "passjkl", Id = 8 });
    users.Add(new User { Name = "Pedro", LastName = "Sanchez", Email = "pedro.s@mail.com", PasswordHash = "passmno", Id = 9 });
    users.Add(new User { Name = "Elena", LastName = "Ramirez", Email = "elena.r@mail.com", PasswordHash = "passpqr", Id = 10 });
    users.Add(new User { Name = "Miguel", LastName = "Torres", Email = "miguel.t@mail.com", PasswordHash = "passtuva", Id = 11 });
    users.Add(new User { Name = "Valeria", LastName = "Vargas", Email = "valeria.v@mail.com", PasswordHash = "passwxy", Id = 12 });
    users.Add(new User { Name = "Jorge", LastName = "Morales", Email = "jorge.m@mail.com", PasswordHash = "passz12", Id = 13 });
    users.Add(new User { Name = "Paula", LastName = "Castro", Email = "paula.c@mail.com", PasswordHash = "pass345", Id = 14 });
    users.Add(new User { Name = "Pablo", LastName = "Flores", Email = "pablo.f@mail.com", PasswordHash = "pass678", Id = 15 });
    users.Add(new User { Name = "Andrea", LastName = "Herrera", Email = "andrea.h@mail.com", PasswordHash = "pass901", Id = 16 });
    users.Add(new User { Name = "Diego", LastName = "Gallo", Email = "diego.g@mail.com", PasswordHash = "pass234", Id = 17 });
    users.Add(new User { Name = "Natalia", LastName = "Ruiz", Email = "natalia.r@mail.com", PasswordHash = "pass567", Id = 18 });
    users.Add(new User { Name = "Fernando", LastName = "Molina", Email = "fernando.m@mail.com", PasswordHash = "pass890", Id = 19 });
    users.Add(new User { Name = "Gabriela", LastName = "Ortega", Email = "gabriela.o@mail.com", PasswordHash = "pass101", Id = 20 });
    users.Add(new User { Name = "Ricardo", LastName = "Soto", Email = "ricardo.s@mail.com", PasswordHash = "pass112", Id = 21 });
    }

    public User? GetUserByEmail(string email)
    {
        return users.FirstOrDefault(user => user.Email == email);
    }


    public List<User> GetUsers()
    {
        return users;
    }

    public User? GetUser(long id)
    {
        User? user = users.Where(user => user.Id == id).FirstOrDefault();
        return user;
    }

    public void Save(User user)
    {
        users.Add(user);
    }

    public void UpdateUser(long IdUser)
    {
        throw new NotImplementedException();
    }

    public void Delete(long IdUser)
    {
        throw new NotImplementedException();
    }
}