public class User : Person
{
    // Exponer Id como propiedad pública para compatibilidad con el resto del código
    public long Id
    {
        get { return this.IdPerson; }
        set { this.IdPerson = value; }
    }

    private string username = "";
    private string email = "";
    private string password = "";
    private Role? role;

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