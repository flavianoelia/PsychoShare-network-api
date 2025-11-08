using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace psychoshare_api.Validations;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly long _maxSize;

    public MaxFileSizeAttribute(long maxSize)
    {
        _maxSize = maxSize;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null && file.Length > _maxSize)
        {
            return new ValidationResult($"El archivo excede el tamaño máximo de {_maxSize / (1024 * 1024)} MB.");
        }

        return ValidationResult.Success;
    }
}