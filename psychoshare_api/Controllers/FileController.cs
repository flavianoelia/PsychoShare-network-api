using Microsoft.AspNetCore.Mvc;
using dao_library.interfaces.media;
using dao_library.entity_framework.media;
using Microsoft.AspNetCore.Authorization;
using psychoshare_api.DTOs.Post;
using System.Security.Claims;
using entity_library.media;
using psychoshare_api.Services;
using psychoshare_api.Configurations;
using psychoshare_api.Services.Interfaces;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly ILogger<FileController> _logger;
    private readonly IDAOImage _daoImage;
    private readonly IDAOPdf _daoPdf;
    private readonly IFileUploadService _fileUploadService;

    public FileController(
        ILogger<FileController> logger,
        IDAOImage daoImage,
        IDAOPdf daoPdf,
        IFileUploadService fileUploadService)
    {
        _logger = logger;
        _daoImage = daoImage;
        _daoPdf = daoPdf;
        _fileUploadService = fileUploadService;
    }
  
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile([FromForm] FileUploadDto dto)
    {
        if (dto.File == null || dto.File.Length == 0)
            return BadRequest("No file uploaded");

        string fileType = dto.File.ContentType.ToLower();
        string fileUrl;

        if (fileType.Contains("image"))
        {
            fileUrl = _fileUploadService.SaveImage(dto.File);

            var image = new Image
            {
                Url = fileUrl,
                Name = dto.File.FileName,
                IdUser = long.TryParse(dto.UserId, out var uid) ? uid : 0,
                ImageType = fileType
            };
            await _daoImage.SaveAsync(image);

            return Ok(new FileResponseDto
            {
                Id = Guid.NewGuid(),
                Url = image.Url,
                FileName = image.Name,
                Size = dto.File.Length,
                Type = "image",
                UploadDate = DateTime.UtcNow,
                UserId = dto.UserId
            });
        }
        else if (fileType.Contains("pdf"))
        {
            fileUrl = _fileUploadService.SavePdf(dto.File);

            var pdf = new Pdf
            {
                Url = fileUrl,
                Name = dto.File.FileName,
                IdUser = long.TryParse(dto.UserId, out var uid) ? uid : 0,
                Type = fileType
            };
            await _daoPdf.SaveAsync(pdf);

            return Ok(new FileResponseDto
            {
                Id = Guid.NewGuid(),
                Url = pdf.Url,
                FileName = pdf.Name,
                Size = dto.File.Length,
                Type = "pdf",
                UploadDate = DateTime.UtcNow,
                UserId = dto.UserId
            });
        }

        return BadRequest("Unsupported file type");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFile(long id)
    {
        var image = await _daoImage.GetByIdAsync(id);
        if (image != null)
        {
            return Ok(new FileResponseDto
            {
                Id = Guid.NewGuid(),
                Url = image.Url,
                FileName = image.Name,
                Size = 0, // si no guardás tamaño en BD
                Type = "image",
                UploadDate = DateTime.UtcNow,
                UserId = image.IdUser.ToString()
            });
        }

        var pdf = await _daoPdf.GetByIdAsync(id);
        if (pdf != null)
        {
            return Ok(new FileResponseDto
            {
                Id = Guid.NewGuid(),
                Url = pdf.Url,
                FileName = pdf.Name,
                Size = 0,
                Type = "pdf",
                UploadDate = DateTime.UtcNow,
                UserId = pdf.IdUser.ToString()
            });
        }

        return NotFound();
    }


    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserFiles(long userId)
    {
        var images = await _daoImage.GetByUserIdAsync(userId);
        var pdfs = await _daoPdf.GetByUserIdAsync(userId);

        var files = images.Select(i => new FileResponseDto
        {
            Id = Guid.NewGuid(),
            Url = i.Url,
            FileName = i.Name,
            Size = 0,
            Type = "image",
            UploadDate = DateTime.UtcNow,
            UserId = i.IdUser.ToString()
        }).Concat(pdfs.Select(p => new FileResponseDto
        {
            Id = Guid.NewGuid(),
            Url = p.Url,
            FileName = p.Name,
            Size = 0,
            Type = "pdf",
            UploadDate = DateTime.UtcNow,
            UserId = p.IdUser.ToString()
        }));

        return Ok(files);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFile(long id)
    {
        var image = await _daoImage.GetByIdAsync(id);
        if (image != null)
        {
            _fileUploadService.DeleteFileByUrl(image.Url);
            await _daoImage.DeleteAsync(id);
            return Ok("Image deleted");
        }

        var pdf = await _daoPdf.GetByIdAsync(id);
        if (pdf != null)
        {
            _fileUploadService.DeleteFileByUrl(pdf.Url);
            await _daoPdf.DeleteAsync(id);
            return Ok("PDF deleted");
        }

        return NotFound();
    }
}
