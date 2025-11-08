namespace psychoshare_api.DTOs.Media.AvatarDto
{
    public class AvatarResponseDTO
    {
        public string Url { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
        public long Size { get; set; } = 0;
    }
}

//Devuelve al cliente la URL, nombre, tipo y tamaño del avatar.