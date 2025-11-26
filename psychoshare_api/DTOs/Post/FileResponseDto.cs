namespace psychoshare_api.DTOs.Post;

public class FileResponseDto
{
public Guid Id { get; set; }
public string Url { get; set; } = string.Empty;
public string FileName { get; set; } = string.Empty;
public long Size { get; set; }
public string Type { get; set; } = string.Empty;
public DateTime UploadDate { get; set; }
public string? UserId { get; set; }
}