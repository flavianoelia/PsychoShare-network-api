using Microsoft.AspNetCore.Mvc;
using psychoshare_api.DTOs.Post;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;
    private readonly DAOFactory _daoFactory;

    public PostController(ILogger<PostController> logger, DAOFactory daoFactory)
    {
        _logger = logger;
        _daoFactory = daoFactory;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest dto)
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

        if (!string.IsNullOrWhiteSpace(dto.Description) && (!System.Text.RegularExpressions.Regex.IsMatch(dto.Description.Trim(), @"^[a-zA-Z0-9\s.,!?()-áéíóúñ]+$") || dto.Description.Trim() == "."))
            errors.Add("Contenido inválido en Description");

        if (!string.IsNullOrWhiteSpace(dto.Title) && (!System.Text.RegularExpressions.Regex.IsMatch(dto.Title.Trim(), @"^[a-zA-Z0-9\s.,!?()-áéíóúñ]+$") || dto.Title.Trim() == "."))
            errors.Add("Contenido inválido en Title");

        if (!string.IsNullOrWhiteSpace(dto.Authorship) && (!System.Text.RegularExpressions.Regex.IsMatch(dto.Authorship.Trim(), @"^[a-zA-Z0-9\s.,!?()-áéíóúñ]+$") || dto.Authorship.Trim() == "."))
            errors.Add("Contenido inválido en Authorship");

        if (!string.IsNullOrWhiteSpace(dto.Resume) && (!System.Text.RegularExpressions.Regex.IsMatch(dto.Resume.Trim(), @"^[a-zA-Z0-9\s.,!?()-áéíóúñ]+$") || dto.Resume.Trim() == "."))
            errors.Add("Contenido inválido en Resume");

        dto.Image = dto.Image ?? string.Empty;
        dto.Pdf = dto.Pdf ?? string.Empty;

        if (errors.Count > 0)
            return BadRequest(errors);

        var post = new Post
        {
            Description = dto.Description!.Trim(),
            Title = dto.Title!.Trim(),
            Authorship = dto.Authorship!.Trim(),
            Resume = dto.Resume!.Trim(),
            UserId = 1,
            NameOwner = "TODO",
            LastnameOwner = "TODO"
        };

        return Ok();
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
                LastnameOwner = post.LastnameOwner
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

            var response = posts.Select(post => new PostResponseDto
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
                LastnameOwner = post.LastnameOwner
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
    public IActionResult GetAllPosts()
    {
        try
        {
            var daoPost = _daoFactory.DaoPost();
            var posts = daoPost.GetAllPosts();

            var response = posts.Select(post => new PostResponseDto
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
                LastnameOwner = post.LastnameOwner
            }).ToList();

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

            // Actualizar propiedades
            existingPost.Description = dto.Description!.Trim();
            existingPost.Title = dto.Title!.Trim();
            existingPost.Authorship = dto.Authorship!.Trim();
            existingPost.Resume = dto.Resume!.Trim();
            
            // Actualizar URLs de imagen y PDF si se proporcionan
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

            // Por ahora usamos UpdatePost como workaround - necesitamos agregar Delete al DAO
            // TODO: Agregar método Delete a DAOPost interface y EFDAOPost
            // Como workaround temporal, usemos el contexto directamente
            return StatusCode(501, "Delete functionality needs to be implemented in DAOPost");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar post {PostId}", id);
            return StatusCode(500, "Error interno del servidor");
        }
    }
}
