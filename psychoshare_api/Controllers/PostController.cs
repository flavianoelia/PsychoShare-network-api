using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Post;
using System.Security.Claims;
using entity_library.media;
using psychoshare_api.Services;
using psychoshare_api.Configurations;
using psychoshare_api.Services.Interfaces;


namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly DAOFactory _daoFactory;
    private readonly IFileUploadService _fileUploadService;

    public PostController(
        ILogger<PostController> logger,
        DAOFactory daoFactory,
        IFileUploadService fileUploadService
    )
    {
        _logger = logger;
        _daoFactory = daoFactory;
        _fileUploadService = fileUploadService;
    }

[Authorize]
[HttpPost]
public IActionResult CreatePost([FromForm] CreatePostRequest dto)
{
    var errors = new List<string>();

    if (string.IsNullOrWhiteSpace(dto.Description) || dto.Description.Trim().Length < 2)
        errors.Add("Campo Description requerido, mínimo 2 caracteres");
    if (string.IsNullOrWhiteSpace(dto.Title) || dto.Title.Trim().Length < 2)
        errors.Add("Campo Title requerido, mínimo 2 caracteres");
    if (string.IsNullOrWhiteSpace(dto.Authorship) || dto.Authorship.Trim().Length < 2)
        errors.Add("Campo Authorship requerido, mínimo 2 caracteres");
    if (string.IsNullOrWhiteSpace(dto.Resume) || dto.Resume.Trim().Length < 2)
        errors.Add("Campo Resume requerido, mínimo 2 caracteres");

    if (errors.Count > 0)
        return BadRequest(errors);

    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (!long.TryParse(userIdClaim, out long currentUserId))
        return Unauthorized("Token inválido o usuario no identificado");

    var user = _daoFactory.DAOUser().GetUser(currentUserId);
    if (user == null)
        return NotFound("Usuario no encontrado");

    var post = new Post
    {
        Description = dto.Description!.Trim(),
        Title = dto.Title!.Trim(),
        Authorship = dto.Authorship!.Trim(),
        Resume = dto.Resume!.Trim(),
        UserId = currentUserId,
        NameOwner = user.Name,
        LastnameOwner = user.LastName
    };

    // Imagen
    if (dto.Image != null && dto.Image.Length > 0)
    {
        try
        {
            var url = _fileUploadService.SaveImage(dto.Image);
            post.Image = new Image
            {
                Url = url,
                Name = dto.Image.FileName,
                IdUser = currentUserId
            };
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al guardar imagen: {ex.Message}");
        }
    }

    // PDF
    if (dto.Pdf != null && dto.Pdf.Length > 0)
    {
        try
        {
            var url = _fileUploadService.SavePdf(dto.Pdf);
            post.Pdf = new Pdf
            {
                Url = url,
                Name = dto.Pdf.FileName,
                IdUser = currentUserId
            };
        }
        catch (Exception ex)
        {
            return BadRequest($"Error al guardar PDF: {ex.Message}");
        }
    }

    var daoPost = _daoFactory.DaoPost();
    daoPost.Save(post);

        return Ok("Post creado exitosamente");
    }


    [HttpGet("{id}")]
    public IActionResult ViewPost(long id)
    {
        try
        {
            var daoPost = _daoFactory.DaoPost();
            var post = daoPost.GetPost(id);

            if (post == null)
                return NotFound($"Post con ID {id} no encontrado");

            var postUser = _daoFactory.DAOUser().GetUser(post.UserId);
            var response = new PostResponseDto
            {
                Id = post.Id,
                Description = post.Description,
                Title = post.Title,
                Authorship = post.Authorship,
                Resume = post.Resume,
                ImageUrl = post.Image?.Url,
                PdfUrl = post.Pdf?.Url,
                UserId = post.UserId,
                NameOwner = post.NameOwner,
                LastnameOwner = post.LastnameOwner,
                AvatarUrl = postUser?.Avatar?.Url,
                CreatedAt = post.CreatedAt
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener post {PostId}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetUserPosts(long userId)
    {
        try
        {
            var daoPost = _daoFactory.DaoPost();
            var posts = daoPost.GetPostFromUser(userId);

            var response = posts.Select(post =>
            {
                var postUser = _daoFactory.DAOUser().GetUser(post.UserId);
                return new PostResponseDto
                {
                    Id = post.Id,
                    Description = post.Description,
                    Title = post.Title,
                    Authorship = post.Authorship,
                    Resume = post.Resume,
                    ImageUrl = post.Image?.Url,
                    PdfUrl = post.Pdf?.Url,
                    UserId = post.UserId,
                    NameOwner = post.NameOwner,
                    LastnameOwner = post.LastnameOwner,
                    AvatarUrl = postUser?.Avatar?.Url,
                    CreatedAt = post.CreatedAt
                };
            }).ToList();

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener posts del usuario {UserId}", userId);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet]
    public IActionResult GetAllPosts([FromQuery] FeedRequestDto? request = null)
    {
        try
        {
            request ??= new FeedRequestDto(); 

            
            if (request.Page < 1) request.Page = 1;
            if (request.Size < 1 || request.Size > 20) request.Size = 10; 

            var daoPost = _daoFactory.DaoPost();
            var (posts, totalCount) = daoPost.GetAllPostsPaginated(
                request.Page, 
                request.Size, 
                request.SearchTerm
            );

            var postDtos = posts.Select(post =>
            {
                var postUser = _daoFactory.DAOUser().GetUser(post.UserId);
                return new PostResponseDto
                {
                    Id = post.Id,
                    Description = post.Description,
                    Title = post.Title,
                    Authorship = post.Authorship,
                    Resume = post.Resume,
                    ImageUrl = post.Image?.Url,
                    PdfUrl = post.Pdf?.Url,
                    UserId = post.UserId,
                    NameOwner = post.NameOwner,
                    LastnameOwner = post.LastnameOwner,
                    AvatarUrl = postUser?.Avatar?.Url,
                    CreatedAt = post.CreatedAt
                };
            }).ToList();

            var response = new PostFeedResponseDto
            {
                Posts = postDtos,
                TotalCount = totalCount,
                Page = request.Page,
                Size = request.Size,
                HasMore = (request.Page * request.Size) < totalCount
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los posts");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPut("{id}")]
    public IActionResult EditPost(long id, [FromBody] EditPostDto dto)
    {
        try
        {
            var daoPost = _daoFactory.DaoPost();
            var existingPost = daoPost.GetPost(id);

            if (existingPost == null)
                return NotFound($"Post con ID {id} no encontrado");

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.Description) || dto.Description.Trim().Length < 2)
                errors.Add("Campo Description requerido, mínimo 2 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Title) || dto.Title.Trim().Length < 2)
                errors.Add("Campo Title requerido, mínimo 2 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Authorship) || dto.Authorship.Trim().Length < 2)
                errors.Add("Campo Authorship requerido, mínimo 2 caracteres");

            if (string.IsNullOrWhiteSpace(dto.Resume) || dto.Resume.Trim().Length < 2)
                errors.Add("Campo Resume requerido, mínimo 2 caracteres");

            if (errors.Count > 0)
                return BadRequest(errors);

    
            existingPost.Description = dto.Description!.Trim();
            existingPost.Title = dto.Title!.Trim();
            existingPost.Authorship = dto.Authorship!.Trim();
            existingPost.Resume = dto.Resume!.Trim();
            
            
            if (dto.Image != null && existingPost.Image != null)
                existingPost.Image.Url = dto.Image;
            
            if (dto.Pdf != null && existingPost.Pdf != null)
                existingPost.Pdf.Url = dto.Pdf;

            daoPost.UpdatePost(id);

            return Ok("Post actualizado exitosamente");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al editar post {PostId}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePost(long id)
    {
        try
        {
            var daoPost = _daoFactory.DaoPost();
            var existingPost = daoPost.GetPost(id);

            if (existingPost == null)
                return NotFound($"Post con ID {id} no encontrado");

            daoPost.Delete(id);
            return Ok($"Post con ID {id} eliminado correctamente");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar post {PostId}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpGet("feed")]
    public IActionResult GetFeed([FromQuery] FeedRequestDto request)
    {
        try
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out long currentUserId))
            {
                return Unauthorized("Token inválido o usuario no identificado");
            }
            
            if (request.Page < 1) request.Page = 1;
            if (request.Size < 1 || request.Size > 20) request.Size = 10; 

            var daoPost = _daoFactory.DaoPost();
            var (posts, totalCount) = daoPost.GetFeedPosts(
                currentUserId,
                request.Page, 
                request.Size, 
                request.SearchTerm
            );

            var postDtos = posts.Select(post =>
            {
                var postUser = _daoFactory.DAOUser().GetUser(post.UserId);
                return new PostResponseDto
                {
                    Id = post.Id,
                    Description = post.Description,
                    Title = post.Title,
                    Authorship = post.Authorship,
                    Resume = post.Resume,
                    ImageUrl = post.Image?.Url,
                    PdfUrl = post.Pdf?.Url,
                    UserId = post.UserId,
                    NameOwner = post.NameOwner,
                    LastnameOwner = post.LastnameOwner,
                    AvatarUrl = postUser?.Avatar?.Url,
                    CreatedAt = post.CreatedAt
                };
            }).ToList();

            var response = new PostFeedResponseDto
            {
                Posts = postDtos,
                TotalCount = totalCount,
                Page = request.Page,
                Size = request.Size,
                HasMore = (request.Page * request.Size) < totalCount
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener feed de posts");
            return StatusCode(500, "Error interno del servidor");
        }
    }

    // ENDPOINT DE SALUD PARA VERIFICAR QUE EL SERVIDOR FUNCIONA
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok(new { status = "OK", timestamp = DateTime.Now, message = "Server is running" });
    }

    // ENDPOINT TEMPORAL PARA PROBAR POST-002
    [HttpGet("feed-test/{userId}")]
    public IActionResult GetFeedTest(int userId, [FromQuery] FeedRequestDto? request = null)
    {
        try
        {
            request ??= new FeedRequestDto();
            if (request.Page < 1) request.Page = 1;
            if (request.Size < 1 || request.Size > 20) request.Size = 10;

            var daoPost = _daoFactory.DaoPost();
            var (posts, totalCount) = daoPost.GetFeedPosts(userId, request.Page, request.Size, request.SearchTerm);

            var postDtos = posts.Select(post =>
            {
                var postUser = _daoFactory.DAOUser().GetUser(post.UserId);
                return new PostResponseDto
                {
                    Id = post.Id,
                    Description = post.Description,
                    Title = post.Title,
                    Authorship = post.Authorship,
                    Resume = post.Resume,
                    ImageUrl = post.Image?.Url,
                    PdfUrl = post.Pdf?.Url,
                    UserId = post.UserId,
                    NameOwner = post.NameOwner,
                    LastnameOwner = post.LastnameOwner,
                    AvatarUrl = postUser?.Avatar?.Url,
                    CreatedAt = post.CreatedAt
                };
            }).ToList();

            var response = new PostFeedResponseDto
            {
                Posts = postDtos,
                TotalCount = totalCount,
                Page = request.Page,
                Size = request.Size,
                HasMore = (request.Page * request.Size) < totalCount
            };

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = ex.Message });
        }
    }
}