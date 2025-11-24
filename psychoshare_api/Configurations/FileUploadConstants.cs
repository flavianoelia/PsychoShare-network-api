namespace psychoshare_api.Configurations
{
    public static class FileUploadConstants
    {
        // Tamaño máximo en bytes (ej. 5 MB)
        public const long MaxImageSize = 5 * 1024 * 1024;

        // Formatos permitidos
        public static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

        // Rutas relativas
        public const string AvatarUploadPath = "uploads/avatars";
        public const string ImageUploadPath = "uploads/images";
        public const string PdfUploadPath = "uploads/pdfs";

        public static readonly string[] AllowedPdfExtensions = { ".pdf" };

        public const long MaxPdfSize = 10 * 1024 * 1024;


        // Ruta física base (se puede combinar con IWebHostEnvironment)
        public const string UploadRoot = "wwwroot";
    }
}