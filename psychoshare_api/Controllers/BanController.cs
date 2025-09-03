using Microsoft.AspNetCore.Mvc;

namespace psychoshare_api.Controllers;

[ApiController]
[Route("[controller]")]
public class BanController : ControllerBase
{
    private readonly ILogger<BanController> _logger;
    private readonly IBanRepository _banRepository;

    public BanController(ILogger<BanController> logger, IBanRepository banRepository)
    {
        _logger = logger;
        _banRepository = banRepository;
    }

    [HttpPost]
    public async Task<ActionResult<BanResponseDto>> BanUser([FromBody] CreateBanDto createBanDto)
    {    
        // Lógica simplificada en el controlador
        if (createBanDto.EndDate <= createBanDto.StartDate)
            return BadRequest("EndDate must be after StartDate");

        var ban = new Ban
        {
            BannedUserId = createBanDto.UserId,
            StartDate = createBanDto.StartDate,
            EndDate = createBanDto.EndDate,
            Reason = createBanDto.Reason,
            IsActive = true
        };
        var idBan = await _banRepository.CreateBanAsync(ban);
        var response = new BanResponseDto
        {
            IdBan = idBan,
            IdPerson = ban.BannedUserId,
            StartDate = ban.StartDate,
            EndDate = ban.EndDate ?? DateTime.MinValue,
            Reason = ban.Reason,
            IsActive = ban.IsActive
        };
        return Ok(response);
    }

    [HttpDelete("{userId}")]
    public async Task<ActionResult<bool>> UnbanUser(long userId)
    {   
        // Mock: desactivar ban activo
        var activeBan = await _banRepository.GetActiveBanByUserIdAsync(userId);
        if (activeBan == null)
            return Ok(false);
        activeBan.IsActive = false;
        var updated = await _banRepository.UpdateBanAsync(activeBan);
        return Ok(updated);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<BanResponseDto>> GetUserBan(long userId)
    {
        var ban = await _banRepository.GetActiveBanByUserIdAsync(userId);
        if (ban == null)
            return NotFound("Ban not found");
        var response = new BanResponseDto
        {
            IdBan = ban.IdBan,
            IdPerson = ban.BannedUserId,
            StartDate = ban.StartDate,
            EndDate = ban.EndDate ?? DateTime.MinValue,
            Reason = ban.Reason,
            IsActive = ban.IsActive
        };
        return Ok(response);
    }

    [HttpGet("active")]
    public async Task<ActionResult<List<BanResponseDto>>> GetActiveBans()
    {
        var bans = await _banRepository.GetAllActiveBansAsync();
        var response = bans.Select(ban => new BanResponseDto
        {
            IdBan = ban.IdBan,
            IdPerson = ban.BannedUserId,
            StartDate = ban.StartDate,
            EndDate = ban.EndDate ?? DateTime.MinValue,
            Reason = ban.Reason,
            IsActive = ban.IsActive
        }).ToList();
        return Ok(response);
    }

    [HttpGet("check/{userId}")]
    public async Task<ActionResult<bool>> CheckBanStatus(long userId)
    {
    var result = await _banRepository.CheckUserIsBannedAsync(userId);
    return Ok(result);
    }
}
