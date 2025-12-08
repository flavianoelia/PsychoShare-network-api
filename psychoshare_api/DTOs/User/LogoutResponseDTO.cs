public class LogoutResponseDTO
{
    public bool Success {get;set;}
    public string Message {get;set;} = string.Empty;
    public long ConnectedUsers {get;set;}
}