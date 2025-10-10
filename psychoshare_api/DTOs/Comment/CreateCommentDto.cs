using System.ComponentModel.DataAnnotations;

namespace psychoshare_api.DTOs.Comment;

public class CreateCommentDto
{
    [Required(ErrorMessage = "UserId is required")]
    public long UserId { get; set; }

    [Required(ErrorMessage = "PostId is required")]
    public long PostId { get; set; }

    [Required(ErrorMessage = "Text is required")]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Text must be between 1 and 1000 characters")]
    public string Text { get; set; } = "";
}