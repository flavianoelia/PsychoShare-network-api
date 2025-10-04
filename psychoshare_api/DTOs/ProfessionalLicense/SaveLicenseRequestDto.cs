namespace psychoshare_api.DTOs.ProfessionalLicense
{
    public class SaveLicenseRequestDto
    {
        public long UserId { get; set; }
        public string LicenseNumber { get; set; } = "";
        public string VerifiedName { get; set; } = "";
        public string VerifiedDni { get; set; } = "";
    }
}