namespace psychoshare_api.DTOs.Post;

public class CreatePostRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Authorship { get; set; }
    public string? Resume { get; set; }
    public string? Image { get; set; }
    public string? Pdf { get; set; }
}