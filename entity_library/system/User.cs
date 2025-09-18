public class User : Person
{
    
    private string username = "";
    private string email = "";
    private string password = "";
    private Role? role;

    public long Id { get; set; } // Needed for MockDAOUser and EF Core

    public string Username
    {
        get { return username; }
        set { username = value; }
    }
    public string Email
    {
        get { return email; }
        set { email = value; }
    }
    public string Password
    {
        get { return password; }
        set { password = value; }
    }
    public Role? Role
    {
        get { return role; }
        set { role = value; }
    }
}