namespace psychoshare_api.DTOs.Post;

public class FileUploadDto
{
[Required]
public IFormFile File { get; set; } = default!;

public string? UserId { get; set; }
public string? Description { get; set; }
}