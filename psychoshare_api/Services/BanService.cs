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
        if (createBanDto.EndDate <= createBanDto.StartDate)
            throw new ArgumentException("EndDate must be after StartDate");

        var ban = new Ban
        {
            BannedUserId = createBanDto.UserId,
            StartDate = createBanDto.StartDate,
            EndDate = createBanDto.EndDate,
            Reason = createBanDto.Reason,
            IsActive = true
        };

        var banIdTask = _banRepository.CreateBanAsync(ban);
        return banIdTask.ContinueWith(task =>
        {
            var idBan = task.Result;
            return new BanResponseDto
            {
                IdBan = idBan,
                IdPerson = ban.BannedUserId,
                StartDate = ban.StartDate,
                EndDate = ban.EndDate ?? DateTime.MinValue,
                Reason = ban.Reason,
                IsActive = ban.IsActive
            };
        });
    }

    public Task<BanResponseDto?> GetBanByUserIdAsync(long userId)
    {
        return _banRepository.GetActiveBanByUserIdAsync(userId)
            .ContinueWith(task =>
            {
                var ban = task.Result;
                if (ban == null) return null;
                return new BanResponseDto
                {
                    IdBan = ban.IdBan,
                    IdPerson = ban.BannedUserId,
                    StartDate = ban.StartDate,
                    EndDate = ban.EndDate ?? DateTime.MinValue,
                    Reason = ban.Reason,
                    IsActive = ban.IsActive
                };
            });
    }

    public Task<List<BanResponseDto>> GetAllActiveBansAsync()
    {
        return _banRepository.GetAllActiveBansAsync()
            .ContinueWith(task =>
            {
                var bans = task.Result;
                return bans.Select(ban => new BanResponseDto
                {
                    IdBan = ban.IdBan,
                    IdPerson = ban.BannedUserId,
                    StartDate = ban.StartDate,
                    EndDate = ban.EndDate ?? DateTime.MinValue,
                    Reason = ban.Reason,
                    IsActive = ban.IsActive
                }).ToList();
            });
    }

    public Task<bool> UnbanUserAsync(long userId)
    {
        return _banRepository.GetActiveBanByUserIdAsync(userId)
            .ContinueWith(task =>
            {
                var ban = task.Result;
                if (ban == null) return false;
                ban.EndDate = DateTime.Now;
                ban.IsActive = false;
                return _banRepository.UpdateBanAsync(ban).Result;
            });
    }

    public Task<bool> CheckUserIsBannedAsync(long userId)
    {
    return _banRepository.CheckUserIsBannedAsync(userId);
    }

    public Task<bool> UpdateBanAsync(long banId, UpdateBanDto updateBanDto)
    {
        return _banRepository.GetBanByIdAsync(banId)
            .ContinueWith(task =>
            {
                var ban = task.Result;
                if (ban == null) return false;
                if (updateBanDto.Reason != null)
                    ban.Reason = updateBanDto.Reason;
                ban.EndDate = updateBanDto.EndDate;
                return _banRepository.UpdateBanAsync(ban).Result;
            });
    }
}
