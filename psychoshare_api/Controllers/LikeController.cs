using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Like;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LikeController : ControllerBase
{
    private readonly ILogger<LikeController> _logger;
    private readonly DAOFactory _daoFactory;

    public LikeController(ILogger<LikeController> logger, DAOFactory daoFactory)
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

    /// <summary>
    /// Toggle like/unlike for a post (Main method)
    /// </summary>
    [HttpPost("toggle")]
    public ActionResult<bool> ToggleLike([FromBody] LikeRequestDto likeRequest)
    {
        try
        {
            var user = _daoFactory.DAOUser().GetUser(likeRequest.UserId);
            if (user == null)
                return NotFound("Usuario no encontrado");

            var post = _daoFactory.DaoPost().GetPost(likeRequest.PostId);
            if (post == null)
                return NotFound("Post no encontrado");

            var isLiked = _daoFactory.DAOLike().ToggleLike(likeRequest.UserId, likeRequest.PostId);
            
            return Ok(isLiked);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error toggling like");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Get like statistics for a post
    /// </summary>
    [HttpGet("stats/{postId}")]
    public ActionResult<LikeStatsDto> GetLikeStats(long postId, [FromQuery] long currentUserId)
    {
        try
        {
            var post = _daoFactory.DaoPost().GetPost(postId);
            if (post == null)
                return NotFound("Post no encontrado");

            var (likeCount, isLikedByCurrentUser) = _daoFactory.DAOLike().GetLikeStats(postId, currentUserId);
            var recentLikes = _daoFactory.DAOLike().GetPostLikes(postId).Take(3).ToList();
            var recentLikerNames = recentLikes
                .Where(l => l.User != null)
                .Select(l => l.User!.Name)
                .ToList();

            var statsDto = new LikeStatsDto
            {
                LikeCount = likeCount,
                IsLikedByCurrentUser = isLikedByCurrentUser,
                RecentLikerNames = recentLikerNames
            };

            return Ok(statsDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener estadísticas de likes");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    /// <summary>
    /// Get all users who liked a post
    /// </summary>
    [HttpGet("post/{postId}")]
    public ActionResult<List<LikeResponseDto>> GetPostLikes(long postId)
    {
        try
        {
            
            var post = _daoFactory.DaoPost().GetPost(postId);
            if (post == null)
                return NotFound("Post no encontrado");

            
            var likes = _daoFactory.DAOLike().GetPostLikes(postId);

            // Convertir a DTOs
            var likeDtos = likes.Select(like => new LikeResponseDto
            {
                Id = like.Id,
                UserId = like.UserId,
                PostId = like.PostId,
                UserName = like.User?.Name ?? "Usuario desconocido",
                CreatedAt = FormatDateTime(GetArgentinaTime())
            }).ToList();

            return Ok(likeDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener likes del post");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    /// <summary>
    /// Get all posts liked by a user
    /// </summary>
    [HttpGet("user/{userId}")]
    public ActionResult<List<LikeResponseDto>> GetUserLikes(long userId)
    {
        try
        {
            
            var user = _daoFactory.DAOUser().GetUser(userId);
            if (user == null)
                return NotFound("Usuario no encontrado");

            
            var likes = _daoFactory.DAOLike().GetUserLikes(userId);

            // Convertir a DTOs
            var likeDtos = likes.Select(like => new LikeResponseDto
            {
                Id = like.Id,
                UserId = like.UserId,
                PostId = like.PostId,
                UserName = like.User?.Name ?? "Usuario desconocido",
                CreatedAt = FormatDateTime(GetArgentinaTime())
            }).ToList();

            return Ok(likeDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener likes del usuario");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    
    [HttpGet("check")]
    public ActionResult<bool> CheckIfUserLikedPost([FromQuery] long userId, [FromQuery] long postId)
    {
        try
        {
            _logger.LogInformation("Checking like for user {UserId} and post {PostId}", userId, postId);
            
            
            if (_daoFactory == null)
            {
                _logger.LogError("DAOFactory is null");
                return StatusCode(500, "DAOFactory no disponible");
            }

            
            var daoLike = _daoFactory.DAOLike();
            if (daoLike == null)
            {
                _logger.LogError("DAOLike is null");
                return StatusCode(500, "DAOLike no disponible");
            }

            _logger.LogInformation("About to call IsLiked method");
            var isLiked = daoLike.IsLiked(userId, postId);
            _logger.LogInformation("IsLiked result: {IsLiked}", isLiked);
            
            return Ok(isLiked);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al verificar like para user {UserId} y post {PostId}", userId, postId);
            return StatusCode(500, "Error interno del servidor");
        }
    }
}
