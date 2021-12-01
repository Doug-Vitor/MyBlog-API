using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class PasswordValidationAttribute : ValidationAttribute
{
    private Regex RequiredChars = new (@"(?=.*[a-zA-Z0-9])(?=.*[-+_!@#$%^&*.,?])");
    private Regex RequiredLength = new(@".{8,}");

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace((string)value))
            return null;

        return (RequiredChars.IsMatch((string)value) && RequiredLength.IsMatch((string)value)) ? ValidationResult.Success
           : new ValidationResult("A senha deve conter pelo menos uma letra minúscula, uma letra maiúscula, um caractere especial e, no total, 8 (oito) caracteres.");
    }
}
