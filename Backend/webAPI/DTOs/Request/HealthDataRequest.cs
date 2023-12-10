using System.ComponentModel.DataAnnotations;

namespace webAPI.DTOs.Request
{
    public class HealthDataRequest
    {
        [Required]
        public float BodyMass { get; set; }

        [Required]
        public float Bmi { get; set; }

        [Required]
        public float BodyFat { get; set; }

        [Required]
        public float LeanBodyMass { get; set; }

        [Required]
        public string? SleepAnalysis { get; set; }
    }
}
