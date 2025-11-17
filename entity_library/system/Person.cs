public class Person
{
    private long idPerson;
    private string name = "";
    private string lastName = "";
    private long? roleId = 1;

    public long Id
    {
        get { return idPerson; }
        set { idPerson = value; }
    }

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

    public long? RoleId
    {
        get { return roleId; }
        set { roleId = value; }
    }
}    