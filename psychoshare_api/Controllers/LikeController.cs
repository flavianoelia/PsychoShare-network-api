using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class LikeController : ControllerBase
{
    private readonly ILogger<LikeController> _logger;

    public LikeController(ILogger<LikeController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public void LikePost()
    {
        // TODO: Like a post
    }

    [HttpDelete]
    public void UnlikePost()
    {
        // TODO: Unlike a post
    }

    [HttpGet("post/{postId}")]
    public void GetPostLikes(int postId)
    {
        // TODO: Get all likes for a post
    }

    [HttpGet("user/{userId}")]
    public void GetUserLikes(int userId)
    {
        // TODO: Get all posts liked by user
    }
}
