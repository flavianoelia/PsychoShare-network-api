public class MockDAOUser : DAOUser
{
    private static List<User> users = new List<User>();
    public User? GetUser(long id)
    {
        //"Entity framework"
        User? user = users.Where(user => user.IdPerson == id).FirstOrDefault();

        return user;
    }

    public void Save(User user)
    {
        users.Add(user);
    }
}