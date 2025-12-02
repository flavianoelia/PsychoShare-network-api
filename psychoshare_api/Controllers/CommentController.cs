using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Comment;
using entity_library.system;
using Microsoft.AspNetCore.Authorization;
using entity_library.media;

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

    private string FormatDateTime(DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    private DateTime GetArgentinaTime()
    {
        var argentinaZone = TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, argentinaZone);
    }

    [HttpPost]
    public ActionResult<CommentResponseDto> CreateComment([FromBody] CreateCommentDto createCommentDto)
    {
        try
        {

            var user = _daoFactory.DAOUser().GetUser(createCommentDto.UserId);
            if (user == null)
                return NotFound("User not found");

            var post = _daoFactory.DaoPost().GetPost(createCommentDto.PostId);
            if (post == null)
                return NotFound("Post not found");

            var comment = new Comment
            {
                UserId = createCommentDto.UserId,
                PostId = createCommentDto.PostId,
                Text = createCommentDto.Text
            };
            _daoFactory.DAOComment().Save(comment);

            var avatar = _daoFactory.DAOAvatar().GetAvatarByUserId(user.Id);
            var responseDto = new CommentResponseDto
            {
                Id = comment.Id,
                Text = comment.Text,
                UserId = comment.UserId,
                UserName = user.Name,
                PostId = comment.PostId,
                CreatedAt = FormatDateTime(GetArgentinaTime()),
                AvatarUrl = avatar?.Url,
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

    [HttpGet("post/{postId}")]
    public ActionResult<CommentsPaginationDto> GetPostComments(long postId)
    {
        try
        {
            var post = _daoFactory.DaoPost().GetPost(postId);
            if (post == null)
                return NotFound("Post not found");
            var comments = _daoFactory.DAOComment().getCommentsFromPost(postId, 2);
            var totalCount = _daoFactory.DAOComment().GetCommentsCount(postId);

            var commentDtos = comments.Select(comment => {
                var avatar = comment.User != null ? _daoFactory.DAOAvatar().GetAvatarByUserId(comment.User.Id) : null;
                return new CommentResponseDto
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    UserId = comment.UserId,
                    UserName = comment.User?.Name ?? "Unknown User",
                    PostId = comment.PostId,
                    CreatedAt = FormatDateTime(GetArgentinaTime()), 
                    AvatarUrl = avatar?.Url,
                };
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

    [HttpGet("post/{postId}/more")]
    public ActionResult<CommentsPaginationDto> GetMoreComments(long postId, [FromQuery] int skip = 2, [FromQuery] int take = 5)
    {
        try
        {
            var post = _daoFactory.DaoPost().GetPost(postId);
            if (post == null)
                return NotFound("Post not found");

            var comments = _daoFactory.DAOComment().getCommentsFromPostPaged(postId, skip, take);
            var totalCount = _daoFactory.DAOComment().GetCommentsCount(postId);

            var commentDtos = comments.Select(comment => {
                var avatar = comment.User != null ? _daoFactory.DAOAvatar().GetAvatarByUserId(comment.User.Id) : null;
                return new CommentResponseDto
                {
                    Id = comment.Id,
                    Text = comment.Text,
                    UserId = comment.UserId,
                    UserName = comment.User?.Name ?? "Unknown User", 
                    PostId = comment.PostId,
                    CreatedAt = FormatDateTime(GetArgentinaTime()),
                    AvatarUrl = avatar?.Url,
                };
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

    [HttpPut("{id}")]
    public ActionResult<CommentResponseDto> EditComment(long id, [FromBody] UpdateCommentDto updateCommentDto)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(updateCommentDto.Text))
                return BadRequest("Comment text is required");

            var existingComment = _daoFactory.DAOComment().GetById(id);
            if (existingComment == null)
                return NotFound("Comment not found");

            _daoFactory.DAOComment().Update(id, updateCommentDto.Text);

            var updatedComment = _daoFactory.DAOComment().GetById(id);
            if (updatedComment == null)
            {
                _logger.LogError("Failed to retrieve updated comment {CommentId}", id);
                return StatusCode(500, "Error updating comment");
            }

            var avatar = updatedComment.User != null ? _daoFactory.DAOAvatar().GetAvatarByUserId(updatedComment.User.Id) : null;
            var responseDto = new CommentResponseDto
            {
                Id = updatedComment.Id,
                Text = updatedComment.Text,
                UserId = updatedComment.UserId,
                UserName = updatedComment.User?.Name ?? "Unknown User",
                PostId = updatedComment.PostId,
                CreatedAt = FormatDateTime(GetArgentinaTime()),
                AvatarUrl = avatar?.Url,
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

    [HttpDelete("{id}")]
    public ActionResult DeleteComment(long id)
    {
        try
        {
            var existingComment = _daoFactory.DAOComment().GetById(id);
            if (existingComment == null)
                return NotFound("Comment not found");

            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!long.TryParse(userIdClaim, out long currentUserId))
            {
                return Unauthorized("Token inválido o usuario no identificado");
            }

            var roleClaimValue = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            long roleId = 1;
            if (!string.IsNullOrEmpty(roleClaimValue))
                long.TryParse(roleClaimValue, out roleId);

            if (existingComment.UserId != currentUserId && roleId < 2)
            {
                return Forbid();
            }

            _daoFactory.DAOComment().Delete(id);

            _logger.LogInformation("Comment {CommentId} deleted by user {UserId}", id, currentUserId);
            return Ok(new { message = "Comment deleted successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting comment {CommentId}", id);
            return StatusCode(500, "Internal server error");
        }
    }
}
