using entity_library.media;
namespace entity_library.system
{
    public class User : Person
    {
        private string email = "";
        private string passwordHash = "";
        private Role? role;
        private Avatar? avatar;

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

        public virtual Role? Role
        {
            get { return this.role; }
            set { this.role = value; }
        }

        public virtual Avatar? Avatar
        {
            get { return this.avatar; }
            set { this.avatar = value; }
        }

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