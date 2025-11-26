using entity_library.system;

namespace entity_library.ReportPolicy;

public class Report
{
    public long Id { get; set; } 
    public long ReporterUserId { get; set; }  
    public long ReportedUserId { get; set; } 
    public string Reason { get; set; } = "";
    public string Details { get; set; } = "";
    public DateTime ReportDate { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Under Review, Resolved, Dismissed
    public string ContentType { get; set; } = ""; // User, Post, Comment
    public long? ContentId { get; set; } 
    public virtual User? ReporterUser { get; set; }
    public virtual User? ReportedUser { get; set; }
    public void ReportUser()
    {
        // Logic for reporting a user
    }
}