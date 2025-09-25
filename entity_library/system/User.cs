public class User : Person
{
    private string name = "";
    private string lastName = "";    
    private string email = "";
    private string passwordHash = "";
    private Role? role;

    public long Id { get; set; }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }
    public string Email
    {
        get { return email; }
        set { email = value; }
    }
    public string PasswordHash
    {
        get { return passwordHash; }
        set { passwordHash = value; }
    }
    public virtual Role? Role { get; set; }
    public virtual Image? Image { get; set; }

}