using System.Text.Json;

namespace psychoshare_api.Services
{
    public class CPPCVerificationService
    {
        private readonly HttpClient _httpClient;
        private const string CPPC_ENDPOINT = "https://www.ceni.net.ar/cppc-backoffice/autogestionmatriculas/ajax-bandeja-nomina-registros-mesa-ayuda.php";

        public CPPCVerificationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "PsychoShare-Verification/1.0");
        }

        public async Task<VerificationResult> VerifyLicenseAsync(string licenseNumber, string lastName, string firstName)
        {
            try
            {
                var formData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("xTomador", lastName),
                    new KeyValuePair<string, string>("xTomador1", firstName),
                    new KeyValuePair<string, string>("xTomador2", licenseNumber),
                    new KeyValuePair<string, string>("page", "1"),
                    new KeyValuePair<string, string>("sid", "0.123456")
                });

                _httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
                
                var response = await _httpClient.PostAsync(CPPC_ENDPOINT, formData);
                var content = await response.Content.ReadAsStringAsync();

                return ParseCPPCResponse(content, licenseNumber, lastName, firstName);
            }
            catch (Exception ex)
            {
                return new VerificationResult
                {
                    IsVerified = false,
                    ErrorMessage = "Error connecting to CPPC system"
                };
            }
        }

        private VerificationResult ParseCPPCResponse(string htmlContent, string licenseNumber, string lastName, string firstName)
        {
            if (string.IsNullOrEmpty(htmlContent))
            {
                return new VerificationResult { IsVerified = false, ErrorMessage = "Empty response" };
            }

            var hasLicense = htmlContent.Contains(licenseNumber.PadLeft(6, '0'));
            var hasName = htmlContent.ToUpper().Contains(lastName.ToUpper()) && 
                         htmlContent.ToUpper().Contains(firstName.ToUpper());

            if (hasLicense && hasName)
            {
                var dni = ExtractDni(htmlContent);
                return new VerificationResult
                {
                    IsVerified = true,
                    VerifiedName = $"{lastName.ToUpper()} {firstName.ToUpper()}",
                    VerifiedDni = dni,
                    LicenseNumber = licenseNumber.PadLeft(6, '0')
                };
            }

            return new VerificationResult { IsVerified = false, ErrorMessage = "License not found" };
        }

        private string ExtractDni(string htmlContent)
        {
            var dniPattern = @"\d{8}";
            var match = System.Text.RegularExpressions.Regex.Match(htmlContent, dniPattern);
            return match.Success ? match.Value : "";
        }
    }

    public class VerificationResult
    {
        public bool IsVerified { get; set; }
        public string VerifiedName { get; set; } = "";
        public string VerifiedDni { get; set; } = "";
        public string LicenseNumber { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
    }
}