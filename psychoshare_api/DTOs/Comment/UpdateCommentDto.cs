using System.ComponentModel.DataAnnotations;

namespace psychoshare_api.DTOs.Comment;

public class UpdateCommentDto
{
    [Required(ErrorMessage = "Text is required")]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Text must be between 1 and 1000 characters")]
    public string Text { get; set; } = "";
}