// Test BCrypt password
#r "nuget: BCrypt.Net-Next, 4.0.3"

using BCrypt.Net;

var password = "Fla1234!";
var storedHash = "$2a$11$x30ixybfTryAk/ta6i6fEuZj6ihhfbAx0i7.keQ57WWKgi2boRcgW";

Console.WriteLine($"Testing password: {password}");
Console.WriteLine($"Against hash: {storedHash}");
Console.WriteLine($"Result: {BCrypt.Verify(password, storedHash)}");

// Generate new hash
var newHash = BCrypt.HashPassword(password);
Console.WriteLine($"\nNew hash for '{password}': {newHash}");
