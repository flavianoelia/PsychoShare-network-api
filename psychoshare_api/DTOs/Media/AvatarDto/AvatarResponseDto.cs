namespace psychoshare_api.DTOs.Media.AvatarDto;
public class AvatarResponseDTO
{
    public string Url { get; set; } = "";
    public string FileName { get; set; } = "";
    public string ContentType { get; set; } = "";
    public long Size { get; set; }
}
//Devuelve al cliente la URL, nombre, tipo y tamaño del avatar.