namespace psychoshare_api.DTOs.Comment;

public class CommentsPaginationDto
{
    public List<CommentResponseDto> Comments { get; set; } = new List<CommentResponseDto>();
    public int TotalCount { get; set; }
    public bool HasMore { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
}