using psychoshare_api.DTOs.User;

namespace psychoshare_api.Services;

public static class MockDataService
{
    public static class Users
    {
        public static List<UserResponseDto> TestUsers = new List<UserResponseDto>
        {
            new UserResponseDto
            {
                IdPerson = 1,
                Name = "Flavio",
                LastName = "Elia",
                Username = "dr.flavio.elia",
                Email = "flavio@psychoshare.net",
                RoleName = "Psychologist",
                CreatedAt = new DateTime(2025, 8, 1)
            },
            new UserResponseDto
            {
                IdPerson = 2,
                Name = "María",
                LastName = "González",
                Username = "dra.maria.gonzalez",
                Email = "maria.gonzalez@psychoshare.net",
                RoleName = "Psychologist",
                CreatedAt = new DateTime(2025, 8, 1, 10, 0, 0)
            },
            new UserResponseDto
            {
                IdPerson = 3,
                Name = "Carlos",
                LastName = "Rodríguez",
                Username = "dr.carlos.rodriguez",
                Email = "carlos.rodriguez@psychoshare.net",
                RoleName = "Psychologist",
                CreatedAt = new DateTime(2025, 8, 1, 11, 0, 0)
            },
            new UserResponseDto
            {
                IdPerson = 4,
                Name = "Roberto",
                LastName = "Spam",
                Username = "roberto.spammer",
                Email = "roberto.spam@psychoshare.net",
                RoleName = "Psychologist",
                CreatedAt = new DateTime(2025, 8, 1, 12, 0, 0)
            }
        };
        
        public static UserResponseDto? GetUserById(long id)
        {
            return TestUsers.FirstOrDefault(u => u.IdPerson == id);
        }
        
        public static UserResponseDto? GetUserByUsername(string username)
        {
            return TestUsers.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
        
        public static List<UserResponseDto> SearchUsers(string? username = null, string? email = null)
        {
            var query = TestUsers.AsQueryable();
            
            if (!string.IsNullOrEmpty(username))
                query = query.Where(u => u.Username.Contains(username, StringComparison.OrdinalIgnoreCase));
                
            if (!string.IsNullOrEmpty(email))
                query = query.Where(u => u.Email.Contains(email, StringComparison.OrdinalIgnoreCase));
                
            return query.ToList();
        }
    }
}
