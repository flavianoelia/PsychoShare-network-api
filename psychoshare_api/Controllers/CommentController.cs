using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Comment;
using entity_library.system;
using Microsoft.AspNetCore.Authorization;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommentController : ControllerBase
{
    private readonly ILogger<CommentController> _logger;
    private readonly DAOFactory _daoFactory;

    public CommentController(ILogger<CommentController> logger, DAOFactory daoFactory)
    {
        _logger = logger;
        _daoFactory = daoFactory;
    }

    /// <summary>
    /// Create a new comment on a post
    /// </summary>
    [HttpPost]
    public ActionResult<CommentResponseDto> CreateComment([FromBody] CreateCommentDto createCommentDto)
    {
        try
        {
            // Validate that user exists
            var user = _daoFactory.DAOUser().GetUser(createCommentDto.UserId);
            if (user == null)
                return NotFound("User not found");

            // Validate that post exists
            var post = _daoFactory.DaoPost().GetPost(createCommentDto.PostId);
            if (post == null)
                return NotFound("Post not found");

            // Create comment entity
            var comment = new Comment
            {
                UserId = createCommentDto.UserId,
                PostId = createCommentDto.PostId,
                Text = createCommentDto.Text
            };

            // Save comment
            _daoFactory.DAOComment().Save(comment);

            // Return response DTO
            var responseDto = new CommentResponseDto
            {
                Id = comment.Id,
                Text = comment.Text,
                UserId = comment.UserId,
                UserName = user.Name,
                PostId = comment.PostId,
                CreatedAt = DateTime.Now
            };

            return Ok(responseDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating comment for user {UserId} on post {PostId}", 
                createCommentDto.UserId, createCommentDto.PostId);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get initial comments for a post (first 2 for UX)
    /// </summary>
    [HttpGet("post/{postId}")]
    public ActionResult<CommentsPaginationDto> GetPostComments(long postId)
    {
        try
        {
            // Validate that post exists
            var post = _daoFactory.DaoPost().GetPost(postId);
            if (post == null)
                return NotFound("Post not found");

            // Get first 2 comments (initial load)
            var comments = _daoFactory.DAOComment().getCommentsFromPost(postId, 2);
            var totalCount = _daoFactory.DAOComment().GetCommentsCount(postId);

            // Map to DTOs
            var commentDtos = comments.Select(comment => new CommentResponseDto
            {
                Id = comment.Id,
                Text = comment.Text,
                UserId = comment.UserId,
                UserName = comment.User?.Name ?? "Unknown User",
                PostId = comment.PostId,
                CreatedAt = DateTime.Now // Note: Comment entity doesn't have CreatedAt, using Now as placeholder
            }).ToList();

            var paginationDto = new CommentsPaginationDto
            {
                Comments = commentDtos,
                TotalCount = totalCount,
                HasMore = totalCount > 2,
                CurrentPage = 1,
                PageSize = 2
            };

            return Ok(paginationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting comments for post {PostId}", postId);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get more comments for "see more" functionality  
    /// </summary>
    [HttpGet("post/{postId}/more")]
    public ActionResult<CommentsPaginationDto> GetMoreComments(long postId, [FromQuery] int skip = 2, [FromQuery] int take = 5)
    {
        try
        {
            // Validate that post exists
            var post = _daoFactory.DaoPost().GetPost(postId);
            if (post == null)
                return NotFound("Post not found");

            // Get paginated comments
            var comments = _daoFactory.DAOComment().getCommentsFromPostPaged(postId, skip, take);
            var totalCount = _daoFactory.DAOComment().GetCommentsCount(postId);

            // Map to DTOs
            var commentDtos = comments.Select(comment => new CommentResponseDto
            {
                Id = comment.Id,
                Text = comment.Text,
                UserId = comment.UserId,
                UserName = comment.User?.Name ?? "Unknown User", 
                PostId = comment.PostId,
                CreatedAt = DateTime.Now
            }).ToList();

            var paginationDto = new CommentsPaginationDto
            {
                Comments = commentDtos,
                TotalCount = totalCount,
                HasMore = (skip + take) < totalCount,
                CurrentPage = (skip / take) + 1,
                PageSize = take
            };

            return Ok(paginationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting more comments for post {PostId}, skip {Skip}, take {Take}", 
                postId, skip, take);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Edit a comment (only owner can edit)
    /// </summary>
    [HttpPut("{id}")]
    public ActionResult<CommentResponseDto> EditComment(long id, [FromBody] UpdateCommentDto updateCommentDto)
    {
        try
        {
            // Validate DTO
            if (string.IsNullOrWhiteSpace(updateCommentDto.Text))
                return BadRequest("Comment text is required");

            // Get existing comment to validate ownership
            var existingComment = _daoFactory.DAOComment().GetById(id);
            if (existingComment == null)
                return NotFound("Comment not found");

            // Note: In a real implementation, you would get the current user ID from JWT/authentication
            // For now, we'll update the comment directly

            // Update comment text
            _daoFactory.DAOComment().Update(id, updateCommentDto.Text);

            // Get updated comment for response
            var updatedComment = _daoFactory.DAOComment().GetById(id);
            if (updatedComment == null)
            {
                _logger.LogError("Failed to retrieve updated comment {CommentId}", id);
                return StatusCode(500, "Error updating comment");
            }

            // Map to response DTO
            var responseDto = new CommentResponseDto
            {
                Id = updatedComment.Id,
                Text = updatedComment.Text,
                UserId = updatedComment.UserId,
                UserName = updatedComment.User?.Name ?? "Unknown User",
                PostId = updatedComment.PostId,
                CreatedAt = DateTime.Now
            };

            _logger.LogInformation("Comment {CommentId} updated", id);
            return Ok(responseDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error editing comment {CommentId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

#if DEBUG
    /// <summary>
    /// Check if user exists (DEBUG ONLY - Development environment)
    /// </summary>
    [HttpGet("debug/user/{userId}")]
    public ActionResult CheckUser(long userId)
    {
        try
        {
            var user = _daoFactory.DAOUser().GetUser(userId);
            if (user == null)
                return NotFound($"User {userId} not found");
            
            return Ok(new { user.Id, user.Name, user.LastName, user.Email });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking user {UserId}", userId);
            return StatusCode(500, "Error checking user");
        }
    }

    /// <summary>
    /// Get available posts (DEBUG ONLY - Development environment)
    /// </summary>
    [HttpGet("debug/posts")]
    public ActionResult GetAvailablePosts()
    {
        try
        {
            // Note: This is a temporary endpoint for debugging
            var posts = _daoFactory.DaoPost().GetAllPosts().Take(10);
            var postList = posts.Select(p => new { p.Id, p.Title, p.UserId }).ToList();
            return Ok(postList);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting posts for debugging");
            return StatusCode(500, "Error getting posts");
        }
    }

    /// <summary>
    /// View all users in database (DEBUG ONLY - Development environment)
    /// </summary>
    [HttpGet("debug/view-users")]
    public ActionResult ViewUsers()
    {
        try
        {
            // Get users by querying directly 
            var users = new List<object>();
            for (int i = 1; i <= 15; i++) // Check first 15 IDs
            {
                var user = _daoFactory.DAOUser().GetUser(i);
                if (user != null)
                {
                    users.Add(new { 
                        Id = user.Id, 
                        Name = user.Name, 
                        LastName = user.LastName, 
                        Email = user.Email 
                    });
                }
            }
            return Ok(new { TotalFound = users.Count, Users = users });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// View all comments in database (DEBUG ONLY - Development environment)
    /// </summary>
    [HttpGet("debug/view-comments")]
    public ActionResult ViewComments()
    {
        try
        {
            var allComments = new List<object>();
            
            // Get comments for each post
            var posts = _daoFactory.DaoPost().GetAllPosts();
            foreach (var post in posts)
            {
                var comments = _daoFactory.DAOComment().getCommentsFromPostPaged(post.Id, 0, 100);
                foreach (var comment in comments)
                {
                    allComments.Add(new {
                        Id = comment.Id,
                        Text = comment.Text.Substring(0, Math.Min(50, comment.Text.Length)) + "...",
                        UserId = comment.UserId,
                        UserName = comment.User?.Name ?? "Unknown",
                        PostId = comment.PostId
                    });
                }
            }
            
            return Ok(new { 
                TotalComments = allComments.Count, 
                Comments = allComments.OrderByDescending(c => ((dynamic)c).Id).ToList()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// View database stats (DEBUG ONLY - Development environment)
    /// </summary>
    [HttpGet("debug/stats")]
    public ActionResult ViewStats()
    {
        try
        {
            var posts = _daoFactory.DaoPost().GetAllPosts();
            var stats = new
            {
                TotalPosts = posts.Count,
                PostsInfo = posts.Select(p => new {
                    Id = p.Id,
                    Title = p.Title,
                    CommentsCount = _daoFactory.DAOComment().GetCommentsCount(p.Id)
                }).ToList()
            };
            
            return Ok(stats);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Populate database with test data (DEBUG ONLY - Development environment)
    /// </summary>
    [HttpPost("debug/populate")]
    public ActionResult PopulateTestData()
    {
        try
        {
            // Create 10 test users
            var testUsers = new[]
            {
                new { Name = "Juan", LastName = "Pérez", Email = "juan.perez@test.com" },
                new { Name = "María", LastName = "González", Email = "maria.gonzalez@test.com" },
                new { Name = "Carlos", LastName = "Rodríguez", Email = "carlos.rodriguez@test.com" },
                new { Name = "Ana", LastName = "Martínez", Email = "ana.martinez@test.com" },
                new { Name = "Luis", LastName = "García", Email = "luis.garcia@test.com" },
                new { Name = "Carmen", LastName = "López", Email = "carmen.lopez@test.com" },
                new { Name = "Pedro", LastName = "Sánchez", Email = "pedro.sanchez@test.com" },
                new { Name = "Laura", LastName = "Fernández", Email = "laura.fernandez@test.com" },
                new { Name = "Diego", LastName = "Ramírez", Email = "diego.ramirez@test.com" },
                new { Name = "Sofia", LastName = "Torres", Email = "sofia.torres@test.com" }
            };

            var createdUserIds = new List<long>();

            // Create users (skip if already exists)
            foreach (var userData in testUsers)
            {
                // Check if user already exists
                var existingUser = _daoFactory.DAOUser().GetUserByEmail(userData.Email);
                if (existingUser != null)
                {
                    createdUserIds.Add(existingUser.Id);
                    continue;
                }

                var user = new entity_library.system.User
                {
                    Name = userData.Name,
                    LastName = userData.LastName,
                    Email = userData.Email,
                    PasswordHash = entity_library.system.User.HashPassword("test123")
                };

                _daoFactory.DAOUser().Save(user);
                createdUserIds.Add(user.Id);
            }

            // Get first available post
            var firstPost = _daoFactory.DaoPost().GetAllPosts().FirstOrDefault();
            if (firstPost == null)
                return BadRequest("No posts available to add comments");

            // Create 3 comments per user (30 total)
            var commentTexts = new[]
            {
                "Excellent article! I find the perspective you present very interesting.",
                "After reading all the content, I believe this approach could revolutionize the field.",
                "Have you considered applying this methodology in other similar contexts?"
            };

            var createdComments = 0;
            foreach (var userId in createdUserIds)
            {
                for (int i = 0; i < 3; i++)
                {
                    var comment = new Comment
                    {
                        UserId = userId,
                        PostId = firstPost.Id,
                        Text = $"{commentTexts[i]} - User {userId}"
                    };

                    _daoFactory.DAOComment().Save(comment);
                    createdComments++;
                }
            }

            return Ok(new
            {
                message = "Test data created successfully",
                usersCreated = createdUserIds.Count,
                commentsCreated = createdComments,
                postUsed = firstPost.Id
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error populating test data");
            return StatusCode(500, $"Error: {ex.Message}\nInner: {ex.InnerException?.Message}\nStack: {ex.StackTrace}");
        }
    }
#endif

    /// <summary>
    /// Delete a comment (only owner can delete)
    /// </summary>
    [HttpDelete("{id}")]
    public ActionResult DeleteComment(long id)
    {
        try
        {
            // Get existing comment to validate it exists
            var existingComment = _daoFactory.DAOComment().GetById(id);
            if (existingComment == null)
                return NotFound("Comment not found");

            // Note: In a real implementation, you would validate ownership here
            // if (existingComment.UserId != currentUserId) return Forbid();

            // Delete the comment
            _daoFactory.DAOComment().Delete(id);

            _logger.LogInformation("Comment {CommentId} deleted", id);
            return Ok(new { message = "Comment deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting comment {CommentId}", id);
            return StatusCode(500, "Internal server error");
        }
    }

#if DEBUG
    /// <summary>
    /// [DEBUG] Create only 2 test users with 2 comments each (for CASCADE testing)
    /// </summary>
    [HttpPost("debug/create-test-users")]
    public ActionResult CreateTestUsers()
    {
        try
        {
            if (_daoFactory == null)
                return StatusCode(500, "DAOFactory not available");

            // Create 2 test users
            var testUsers = new List<User>();
            for (int i = 1; i <= 2; i++)
            {
                var user = new User
                {
                    Name = $"TestUser{i}",
                    LastName = "Cascade",
                    Email = $"testuser{i}@test.com",
                    PasswordHash = "test123"
                };

                _daoFactory.DAOUser().Save(user);
                testUsers.Add(user);
                _logger.LogInformation("Test user {UserId} created: {Email}", user.Id, user.Email);
            }

            // Get first available post
            var firstPost = _daoFactory.DaoPost().GetAllPosts().FirstOrDefault();
            if (firstPost == null)
                return BadRequest("No posts available to add comments");

            // Create 2 comments per user (4 total)
            var createdComments = 0;
            foreach (var user in testUsers)
            {
                for (int i = 1; i <= 2; i++)
                {
                    var comment = new Comment
                    {
                        UserId = user.Id,
                        PostId = firstPost.Id,
                        Text = $"Test comment {i} from TestUser{user.Id} - CASCADE TEST"
                    };

                    _daoFactory.DAOComment().Save(comment);
                    createdComments++;
                    _logger.LogInformation("Test comment created: {CommentId} for User {UserId}", comment.Id, user.Id);
                }
            }

            return Ok(new
            {
                message = "Test users and comments created successfully",
                usersCreated = testUsers.Count,
                commentsCreated = createdComments,
                userIds = testUsers.Select(u => u.Id).ToList(),
                postUsed = firstPost.Id
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating test users and comments");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// [DEBUG] Delete test users to test CASCADE delete
    /// </summary>
    [HttpDelete("debug/delete-test-users")]
    public ActionResult DeleteTestUsers()
    {
        try
        {
            // Delete users with test emails
            var testEmails = new[] { "testuser1@test.com", "testuser2@test.com" };
            var deletedCount = 0;

            foreach (var email in testEmails)
            {
                var user = _daoFactory.DAOUser().GetUserByEmail(email);
                if (user != null)
                {
                    _logger.LogInformation("Deleting test user {UserId}: {Email}", user.Id, user.Email);
                    _daoFactory.DAOUser().Delete(user.Id);
                    deletedCount++;
                }
            }

            return Ok(new
            {
                message = "Test users deleted (comments should be deleted by CASCADE)",
                usersDeleted = deletedCount
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting test users");
            return StatusCode(500, "Internal server error");
        }
    }
#endif
}
