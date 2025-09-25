using System.Reflection.Metadata;
using entity_library.system;

public class Comment
{
    public long Id { get; set; }
    public string Text { get; set; }
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public long PostId { get; set; }
    public virtual Post Post { get; set; }
}