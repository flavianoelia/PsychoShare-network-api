
using System.Reflection.Metadata;

public class MockDAOUser : DAOUser
{
    private static List<User> users = new List<User>();

    public MockDAOUser()
    {
        users.Add(new User { Name = "Pepe", LastName = "Roldan", Email = "pepe.com", Password = "12345", Id = 1, Username = "pepeHD" });
        users.Add(new User { Name = "Ana", LastName = "Gomez", Email = "ana.gomez@mail.com", Password = "pass123", Id = 2, Username = "anag" });
        users.Add(new User { Name = "Luis", LastName = "Perez", Email = "luis.perez@mail.com", Password = "pass456", Id = 3, Username = "luisp" });
        users.Add(new User { Name = "Maria", LastName = "Lopez", Email = "maria.lopez@mail.com", Password = "pass789", Id = 4, Username = "marial" });
        users.Add(new User { Name = "Carlos", LastName = "Martinez", Email = "carlos.m@mail.com", Password = "passabc", Id = 5, Username = "carlosm" });
        users.Add(new User { Name = "Sofia", LastName = "Rodriguez", Email = "sofia.r@mail.com", Password = "passdef", Id = 6, Username = "sofiar" });
        users.Add(new User { Name = "Javier", LastName = "Fernandez", Email = "javier.f@mail.com", Password = "passghi", Id = 7, Username = "javierf" });
        users.Add(new User { Name = "Laura", LastName = "Diaz", Email = "laura.d@mail.com", Password = "passjkl", Id = 8, Username = "laurad" });
        users.Add(new User { Name = "Pedro", LastName = "Sanchez", Email = "pedro.s@mail.com", Password = "passmno", Id = 9, Username = "pedros" });
        users.Add(new User { Name = "Elena", LastName = "Ramirez", Email = "elena.r@mail.com", Password = "passpqr", Id = 10, Username = "elenar" });
        users.Add(new User { Name = "Miguel", LastName = "Torres", Email = "miguel.t@mail.com", Password = "passtuva", Id = 11, Username = "miguelt" });
        users.Add(new User { Name = "Valeria", LastName = "Vargas", Email = "valeria.v@mail.com", Password = "passwxy", Id = 12, Username = "valeriav" });
        users.Add(new User { Name = "Jorge", LastName = "Morales", Email = "jorge.m@mail.com", Password = "passz12", Id = 13, Username = "jorgem" });
        users.Add(new User { Name = "Paula", LastName = "Castro", Email = "paula.c@mail.com", Password = "pass345", Id = 14, Username = "paulac" });
        users.Add(new User { Name = "Pablo", LastName = "Flores", Email = "pablo.f@mail.com", Password = "pass678", Id = 15, Username = "pablof" });
        users.Add(new User { Name = "Andrea", LastName = "Herrera", Email = "andrea.h@mail.com", Password = "pass901", Id = 16, Username = "andreah" });
        users.Add(new User { Name = "Diego", LastName = "Gallo", Email = "diego.g@mail.com", Password = "pass234", Id = 17, Username = "diegog" });
        users.Add(new User { Name = "Natalia", LastName = "Ruiz", Email = "natalia.r@mail.com", Password = "pass567", Id = 18, Username = "nataliar" });
        users.Add(new User { Name = "Fernando", LastName = "Molina", Email = "fernando.m@mail.com", Password = "pass890", Id = 19, Username = "fernandom" });
        users.Add(new User { Name = "Gabriela", LastName = "Ortega", Email = "gabriela.o@mail.com", Password = "pass101", Id = 20, Username = "gabrielao" });
        users.Add(new User { Name = "Ricardo", LastName = "Soto", Email = "ricardo.s@mail.com", Password = "pass112", Id = 21, Username = "ricardos" });
    }

    public User? GetUserByEmail(string email)
    {
        return users.FirstOrDefault(user => user.Email == email);
    }

    public User? GetUserByUsername(string username)
    {
        return users.FirstOrDefault(user => user.Username == username);
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