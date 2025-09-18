public class Report
{
    private string name = "";
    private string lastname = "";
    private DateTime reportDate;

    public long Id { get; set ; }
    public string Name { get { return this.name; } set { this.name = value; } }
    public string Lastname { get { return this.lastname; } set { this.lastname = value; } }
    public DateTime ReportDate { get { return this.reportDate; } set { this.reportDate = value; } }

    public void ReportUser()
    {
        
    }
}