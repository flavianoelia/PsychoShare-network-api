using System.ComponentModel.DataAnnotations;

namespace psychoshare_api.DTOs.Comment;

public class EditCommentDto
{
    [Required(ErrorMessage = "Comment ID is required")]
    public long Id { get; set; }

    [Required(ErrorMessage = "Text is required")]
    [StringLength(1000, MinimumLength = 1, ErrorMessage = "Text must be between 1 and 1000 characters")]
    public string Text { get; set; } = "";
}