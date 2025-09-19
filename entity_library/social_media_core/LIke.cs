using System.Reflection.Metadata;

public class Like
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long PostId { get; set; }
    public virtual Post Post { get; set; }
    public virtual User User { get; set; }

}