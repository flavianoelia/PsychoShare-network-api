namespace psychoshare_api.DTOs.Ban;

public class BanPagedResponseDto
{
    public List<BanResponseDto> Bans { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
    public bool HasMore { get; set; }
}