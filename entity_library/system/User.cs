namespace entity_library.system
{
    public class User : Person
    {
        private string name = "";
        private string lastName = "";
        private string email = "";
        private string passwordHash = "";
        private Role? role;

        public long Id { get; set; }

    public new string Name
        {
            get { return name; }
            set { name = value; }
        }
    public new string LastName
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
        public static string HashPassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainPassword);
        }

        public static bool VerifyPassword(string plainPassword, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, passwordHash);
        }
    }
}