using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;

    public PostController(ILogger<PostController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public void CreatePost()
    {
        // TODO: Create new post
    }

        [HttpPost("{postId}/comment")]
        public IActionResult CreateComment(int postId)
        {
            // Create comment on post
            return Ok();
        }

        [HttpGet("{postId}/comments")]
        public IActionResult GetPostComments(int postId)
        {
            // Get all comments for a post
            return Ok();
        }

        [HttpPut("comment/{id}")]
        public IActionResult EditComment(int id)
        {
            // Edit comment
            return Ok();
        }

        [HttpDelete("comment/{id}")]
        public IActionResult DeleteComment(int id)
        {
            // Delete comment
            return Ok();
        }

        [HttpPost("{postId}/like")]
        public IActionResult LikePost(int postId)
        {
            // Like a post
            return Ok();
        }

        [HttpDelete("{postId}/like")]
        public IActionResult UnlikePost(int postId)
        {
            // Unlike a post
            return Ok();
        }

        [HttpGet("{postId}/likes")]
        public IActionResult GetPostLikes(int postId)
        {
            // Get all likes for a post
            return Ok();
        }

        [HttpGet("user/{userId}/likes")]
        public IActionResult GetUserLikes(int userId)
        {
            // Get all posts liked by user
            return Ok();
        }

        [HttpPost("{id}/cover-image")]
        public void UploadCoverImage(int id, IFormFile image)
        {
            // Upload cover image for post
        }

        [HttpPost("{id}/pdf")]
        public void UploadPdf(int id, IFormFile pdf)
        {
            // Upload PDF for post
        }

        [HttpGet("{id}/cover-image")]
        public void GetCoverImage(int id)
        {
            // Get cover image for post
        }

        [HttpGet("{id}/pdf")]
        public void GetPdf(int id)
        {
            // Get PDF for post
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
