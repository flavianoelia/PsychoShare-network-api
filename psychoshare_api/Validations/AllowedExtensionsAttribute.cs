using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Linq;
using psychoshare_api.Configurations;

namespace psychoshare_api.Validations;

public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();


            if (!FileUploadConstants.AllowedImageExtensions.Contains(extension))
            {
                return new ValidationResult($"Extensión no permitida: {extension}. Solo se permiten: {string.Join(", ", FileUploadConstants.AllowedImageExtensions)}");
            }
        }
        return ValidationResult.Success;
    }
}