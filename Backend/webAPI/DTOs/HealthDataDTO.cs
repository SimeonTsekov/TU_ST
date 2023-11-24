using webAPI.DTOs.Request;

namespace webAPI.DTOs
{
    public class HealthDataDTO
    {
        public int HealthDataId { get; set; }
        public int UserId { get; set; }
        public float BodyMass { get; set; }
        public float BMI { get; set; }
        public float BodyFat { get; set; }
        public float LeanBodyMass { get; set; }
        public string? SleepAnalysis { get; set; }
        // Navigation property to the User
        public virtual UserDTO? User { get; set; }
    }
}
