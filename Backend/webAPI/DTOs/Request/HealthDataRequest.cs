using System.ComponentModel.DataAnnotations;

namespace webAPI.DTOs.Request
{
    public class HealthDataRequest
    {
        [Range(0, float.MaxValue, ErrorMessage = "BodyMass must be a positive number.")]
        public float BodyMass { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Bmi must be a positive number.")]
        public float Bmi { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "BodyFat must be a positive number.")]
        public float BodyFat { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "LeanBodyMass must be a positive number.")]
        public float LeanBodyMass { get; set; }

        [Required(ErrorMessage = "SleepAnalysis is required.")]
        public string? SleepAnalysis { get; set; }
    }
}
