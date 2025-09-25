using System;
using Xunit;
using entity_library.system;

namespace entity_library.Tests
{
    public class UserTests
    {
        [Fact]
        public void HashPassword_And_VerifyPassword_WorksCorrectly()
        {
            // Arrange
            string password = "TestPassword123!";

            // Act
            string hash = User.HashPassword(password);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(hash));
            Assert.True(User.VerifyPassword(password, hash));
            Assert.False(User.VerifyPassword("WrongPassword", hash));
        }
    }
}
