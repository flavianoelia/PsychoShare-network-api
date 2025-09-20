public class Report
{
    public long Id { get; set; } 
    public long ReporterUserId { get; set; }  // Usuario que hace el reporte
    public long ReportedUserId { get; set; } // Usuario reportado
    public string Reason { get; set; }
    public string Details { get; set; }
    public DateTime ReportDate { get; set; }
    public string Status { get; set; } // Pending, Under Review, Resolved, Dismissed
    public string ContentType { get; set; } // User, Post, Comment
    public long? ContentId { get; set; } // ID del contenido reportado (opcional)
    public virtual User? ReporterUser { get; set; }
    public virtual User? ReportedUser { get; set; }
    public void ReportUser()
    {
        // Logic for reporting a user
    }
}