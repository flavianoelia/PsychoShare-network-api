using Microsoft.AspNetCore.Mvc;
using entity_library.ReportPolicy;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class BanController : ControllerBase
{
    private readonly ILogger<BanController> _logger;
    private readonly DAOFactory _daoFactory;

    public BanController(ILogger<BanController> logger, DAOFactory daoFactory)
    {
        _logger = logger;
        _daoFactory = daoFactory;
    }

    [HttpPost]
    public ActionResult<BanResponseDto> BanUser([FromBody] CreateBanDto createBanDto)
    {
        
        if (createBanDto.BannedUserId == createBanDto.BannedByAdminId)
            return BadRequest("Un usuario no puede banearse a sí mismo");

        
        var user = _daoFactory.DAOUser().GetUser(createBanDto.BannedUserId);
        if (user == null)
            return NotFound("Usuario a banear no encontrado");

        
        var admin = _daoFactory.DAOUser().GetUser(createBanDto.BannedByAdminId);
        if (admin == null)
            return NotFound("Usuario administrador no encontrado");

        
        var existingBan = _daoFactory.DAOBan().CheckBanStatus(createBanDto.BannedUserId);
        if (existingBan)
            return BadRequest("El usuario ya está baneado activamente");

        
        if (createBanDto.EndDate.HasValue && createBanDto.EndDate <= createBanDto.StartDate)
            return BadRequest("La fecha de fin debe ser mayor que la fecha de inicio");

        
        if (createBanDto.StartDate < DateTime.Now.AddMinutes(-1)) // Tolerancia de 1 minuto
            return BadRequest("La fecha de inicio no puede ser en el pasado");

        
        var ban = new Ban
        {
            BannedUserId = createBanDto.BannedUserId,
            BannedByAdminId = createBanDto.BannedByAdminId,
            BanType = createBanDto.BanType,
            RelatedReportId = createBanDto.RelatedReportId,
            StartDate = createBanDto.StartDate,
            EndDate = createBanDto.EndDate,
            Reason = createBanDto.Reason,
            IsActive = true
        };

        _daoFactory.DAOBan().Save(ban);

        var response = new BanResponseDto
        {
            Id = ban.Id,
            BannedUserId = ban.BannedUserId,
            BannedByAdminId = ban.BannedByAdminId,
            BanType = ban.BanType,
            RelatedReportId = ban.RelatedReportId,
            StartDate = ban.StartDate,
            EndDate = ban.EndDate,
            Reason = ban.Reason,
            IsActive = ban.IsActive
        };

        return Ok(response);
    }

    [HttpDelete("{userId}")]
    public ActionResult<bool> UnbanUser(long userId)
    {
        
        var user = _daoFactory.DAOUser().GetUser(userId);
        if (user == null)
            return NotFound("Usuario no encontrado");

        var ban = _daoFactory.DAOBan().GetUserBan(userId);
        if (ban == null)
            return NotFound("No se encontró un ban para este usuario");

        
        if (!ban.IsActive)
            return BadRequest("El usuario ya no está baneado");

        // Desactivar el ban
        ban.IsActive = false;
        ban.EndDate = DateTime.Now;
        _daoFactory.DAOBan().Update(ban);

        return Ok(true);
    }

    [HttpGet("{userId}")]
    public ActionResult<BanResponseDto> GetUserBan(long userId)
    {
        var ban = _daoFactory.DAOBan().GetUserBan(userId);
        if (ban == null)
            return NotFound("No se encontró un ban para este usuario");

        var response = new BanResponseDto
        {
            Id = ban.Id,
            BannedUserId = ban.BannedUserId,
            BannedByAdminId = ban.BannedByAdminId,
            BanType = ban.BanType,
            RelatedReportId = ban.RelatedReportId,
            StartDate = ban.StartDate,
            EndDate = ban.EndDate,
            Reason = ban.Reason,
            IsActive = ban.IsActive
        };

        return Ok(response);
    }

    [HttpGet("active")]
    public ActionResult<List<BanResponseDto>> GetActiveBans()
    {
        var bans = _daoFactory.DAOBan().GetActiveBans();
        var response = bans.Select(b => new BanResponseDto
        {
            Id = b.Id,
            BannedUserId = b.BannedUserId,
            BannedByAdminId = b.BannedByAdminId,
            BanType = b.BanType,
            RelatedReportId = b.RelatedReportId,
            StartDate = b.StartDate,
            EndDate = b.EndDate,
            Reason = b.Reason,
            IsActive = b.IsActive
        }).ToList();

        return Ok(response);
    }

    [HttpGet("check/{userId}")]
    public ActionResult<bool> CheckBanStatus(long userId)
    {
        var isBanned = _daoFactory.DAOBan().CheckBanStatus(userId);
        return Ok(isBanned);
    }
}
