using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace psychoshare_api.Validations;

public class AllowedMimeTypesAttribute : ValidationAttribute
{
    private readonly string[] _allowedMimeTypes;

    public AllowedMimeTypesAttribute(string[] allowedMimeTypes)
    {
        _allowedMimeTypes = allowedMimeTypes;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var mime = file.ContentType?.ToLowerInvariant();

            if (!_allowedMimeTypes.Contains(mime))
            {
                return new ValidationResult($"Tipo MIME no permitido: {mime}. " +
                    $"Solo se permiten: {string.Join(", ", _allowedMimeTypes)}");
            }
        }

        return ValidationResult.Success;
    }
}
