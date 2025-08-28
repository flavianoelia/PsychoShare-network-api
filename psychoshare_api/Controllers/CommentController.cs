using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ILogger<CommentController> _logger;

    public CommentController(ILogger<CommentController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public void CreateComment()
    {
        // TODO: Create comment on post
    }

    [HttpGet("post/{postId}")]
    public void GetPostComments(int postId)
    {
        // TODO: Get all comments for a post
    }

    [HttpPut("{id}")]
    public void EditComment(int id)
    {
        // TODO: Edit comment
    }

    [HttpDelete("{id}")]
    public void DeleteComment(int id)
    {
        // TODO: Delete comment
    }
}
