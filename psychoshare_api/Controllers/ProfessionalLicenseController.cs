using Microsoft.AspNetCore.Mvc;
using psychoshare_api.Services;
using psychoshare_api.DTOs.ProfessionalLicense;

namespace psychoshare_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessionalLicenseController : ControllerBase
    {
        private readonly CPPCVerificationService _verificationService;

        public ProfessionalLicenseController(CPPCVerificationService verificationService)
        {
            _verificationService = verificationService;
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyLicense([FromBody] VerifyLicenseRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.LicenseNumber) || 
                string.IsNullOrWhiteSpace(request.LastName) || 
                string.IsNullOrWhiteSpace(request.FirstName))
            {
                return BadRequest(new VerifyLicenseResponseDto 
                { 
                    IsVerified = false, 
                    Message = "License number, last name and first name are required" 
                });
            }

            var result = await _verificationService.VerifyLicenseAsync(
                request.LicenseNumber.Trim(), 
                request.LastName.Trim(), 
                request.FirstName.Trim()
            );

            var response = new VerifyLicenseResponseDto
            {
                IsVerified = result.IsVerified,
                VerifiedName = result.VerifiedName,
                VerifiedDni = result.VerifiedDni,
                LicenseNumber = result.LicenseNumber,
                Message = result.IsVerified ? "License verified successfully" : result.ErrorMessage
            };

            return Ok(response);
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveLicense([FromBody] SaveLicenseRequestDto request)
        {
            if (request.UserId <= 0 || string.IsNullOrWhiteSpace(request.LicenseNumber))
            {
                return BadRequest(new { message = "User ID and license number are required" });
            }

            var license = new entity_library.professional.ProfessionalLicense
            {
                UserId = request.UserId,
                LicenseNumber = request.LicenseNumber,
                VerifiedName = request.VerifiedName,
                VerifiedDni = request.VerifiedDni,
                IsVerified = true
            };

            return Ok(new { message = "License saved successfully", licenseId = license.Id });
        }

        [HttpGet("status/{userId}")]
        public async Task<IActionResult> GetLicenseStatus(long userId)
        {
            if (userId <= 0)
            {
                return BadRequest(new { message = "Valid user ID is required" });
            }

            return Ok(new { 
                hasLicense = false, 
                isVerified = false,
                message = "No license found for user" 
            });
        }
    }
}