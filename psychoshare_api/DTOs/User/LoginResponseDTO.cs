public class LoginResponseDTO
{
    public bool success { get; set; }
    public required string message { get; set; }
    public required string email { get; set; }
    public required long userId { get; set; }
    public required string token { get; set; }
    public required string role { get; set; }
}