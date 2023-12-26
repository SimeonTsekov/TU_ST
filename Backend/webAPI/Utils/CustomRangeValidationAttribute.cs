using System.ComponentModel.DataAnnotations;

namespace webAPI.Utils
{
    public class CustomRangeValidationAttribute : ValidationAttribute
    {
        private readonly int _min;
        private readonly int _max;

        public CustomRangeValidationAttribute(int min, int max, string errorMessage) : base(errorMessage)
        {
            _min = min;
            _max = max;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var number = (int) (value ?? -1);

            if (number == -1 || (number >= _min && number <= _max))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(this.ErrorMessage);
            }
        }
    }
}
