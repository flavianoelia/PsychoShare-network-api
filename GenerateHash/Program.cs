using entity_library.system;

Console.WriteLine("=== GENERADOR DE HASH BCrypt ===");
Console.WriteLine();

// Generar hash para Fla1234!
var password1 = "Fla1234!";
var hash1 = User.HashPassword(password1);
Console.WriteLine($"Password: {password1}");
Console.WriteLine($"Hash: {hash1}");
Console.WriteLine();

// Verificar el hash actual de la BD
var currentHash = "$2a$11$x30ixybfTryAk/ta6i6fEuZj6ihhfbAx0i7.keQ57WWKgi2boRcgW";
var isValid = User.VerifyPassword(password1, currentHash);
Console.WriteLine($"¿El hash actual de la BD es válido para '{password1}'?: {isValid}");
Console.WriteLine();

// Probar con la contraseña vieja
var oldPassword = "1234";
var hash2 = User.HashPassword(oldPassword);
Console.WriteLine($"Password: {oldPassword}");
Console.WriteLine($"Hash: {hash2}");
