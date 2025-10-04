namespace psychoshare_api.DTOs.ProfessionalLicense
{
    public class VerifyLicenseResponseDto
    {
        public bool IsVerified { get; set; }
        public string VerifiedName { get; set; } = "";
        public string VerifiedDni { get; set; } = "";
        public string LicenseNumber { get; set; } = "";
        public string Message { get; set; } = "";
    }
}