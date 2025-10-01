using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Post;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;

    public PostController(ILogger<PostController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest dto)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Description) || dto.Description.Trim().Length < 2)
            errors.Add("Campo Description requerido, mínimo 2 caracteres");

        if (string.IsNullOrWhiteSpace(dto.Title) || dto.Title.Trim().Length < 2)
            errors.Add("Campo Title requerido, mínimo 2 caracteres");

        if (errors.Count > 0)
            return BadRequest(errors);

        return Ok();
    }

    [HttpGet("{id}")]
    public void ViewPost(int id)
    {
        // TODO: View specific post
    }

    [HttpGet("user/{userId}")]
    public void GetUserPosts(int userId)
    {
        // TODO: Get all posts by user
    }

    [HttpGet]
    public void GetAllPosts()
    {
        // TODO: Get all posts (feed)
    }

    [HttpPut("{id}")]
    public void EditPost(int id)
    {
        // TODO: Edit post
    }

    [HttpDelete("{id}")]
    public void DeletePost(int id)
    {
        // TODO: Delete post
    }
}
