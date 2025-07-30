public interface IBanService
{
    Task<BanResponseDto> CreateBanAsync(CreateBanDto createBanDto);
    Task<BanResponseDto?> GetBanByUserIdAsync(long userId);
    Task<List<BanResponseDto>> GetAllActiveBansAsync();
    Task<bool> UnbanUserAsync(long userId);
    Task<bool> CheckUserIsBannedAsync(long userId);
    Task<bool> UpdateBanAsync(long banId, UpdateBanDto updateBanDto);
}

public class BanService : IBanService
{
    private readonly IBanRepository _banRepository;

    public BanService(IBanRepository banRepository)
    {
        _banRepository = banRepository;
    }

    public Task<BanResponseDto> CreateBanAsync(CreateBanDto createBanDto)
    {
        // TODO: Validate ban dates
        // TODO: Convert DTO to entity
        // TODO: Save to database
        // TODO: Return response DTO
        throw new NotImplementedException();
    }

    public Task<BanResponseDto?> GetBanByUserIdAsync(long userId)
    {
        // TODO: Get ban from repository
        // TODO: Convert entity to DTO
        throw new NotImplementedException();
    }

    public Task<List<BanResponseDto>> GetAllActiveBansAsync()
    {
        // TODO: Get all active bans
        // TODO: Convert entities to DTOs
        throw new NotImplementedException();
    }

    public Task<bool> UnbanUserAsync(long userId)
    {
        // TODO: Find active ban for user
        // TODO: Update end date to now
        throw new NotImplementedException();
    }

    public Task<bool> CheckUserIsBannedAsync(long userId)
    {
        // TODO: Check if user has active ban
        throw new NotImplementedException();
    }

    public Task<bool> UpdateBanAsync(long banId, UpdateBanDto updateBanDto)
    {
        // TODO: Get existing ban
        // TODO: Update properties
        // TODO: Save changes
        throw new NotImplementedException();
    }
}
