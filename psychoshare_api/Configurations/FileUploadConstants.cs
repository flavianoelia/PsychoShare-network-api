namespace psychoshare_api.Configurations
{
    public static class FileUploadConstants
    {
        // Tamaño máximo en bytes (ej. 20 MB)
        public const long MaxImageSize = 20 * 1024 * 1024;

        // Formatos permitidos
        public static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

        // Rutas relativas
        public const string AvatarUploadPath = "uploads/avatars";
        public const string ImageUploadPath = "uploads/images";
        public const string PdfUploadPath = "uploads/pdfs";

        public static readonly string[] AllowedPdfExtensions = { ".pdf" };

        public const long MaxPdfSize = 50 * 1024 * 1024;


        // Ruta física base (se puede combinar con IWebHostEnvironment)
        public const string UploadRoot = "wwwroot";
    }
}