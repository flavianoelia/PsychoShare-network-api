namespace psychoshare_api.DTOs.ProfessionalLicense
{
    public class VerifyLicenseRequestDto
    {
        public string LicenseNumber { get; set; } = "";
        public string LastName { get; set; } = "";
        public string FirstName { get; set; } = "";
    }
}