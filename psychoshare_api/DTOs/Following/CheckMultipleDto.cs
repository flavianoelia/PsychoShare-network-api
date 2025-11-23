namespace psychoshare_api.DTOs.Following;

public class CheckMultipleDto
{
    public List<long> UserIds { get; set; } = new();
}
