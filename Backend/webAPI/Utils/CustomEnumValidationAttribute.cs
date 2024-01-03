using System.ComponentModel.DataAnnotations;

namespace webAPI.Utils
{
    public class CustomEnumValidationAttribute : ValidationAttribute
    {
        private readonly Type _enumType;

        public CustomEnumValidationAttribute(Type enumType, string errorMessage) : base(errorMessage)
        {
            this._enumType = enumType;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            Enum.TryParse(_enumType, value?.ToString()!, true, out var result);
            return result != null ? ValidationResult.Success : new ValidationResult(this.ErrorMessage);
        }
    }
}
