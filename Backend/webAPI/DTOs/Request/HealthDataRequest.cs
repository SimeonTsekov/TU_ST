namespace webAPI.DTOs.Request
{
    public class HealthDataRequest
    {
        public float BodyMass { get; set; }
        public float BMI { get; set; }
        public float BodyFat { get; set; }
        public float LeanBodyMass { get; set; }
        public string SleepAnalysis { get; set; } = null!;
    }
}
