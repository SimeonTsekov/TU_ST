namespace webAPI.DTOs.Response
{
    public class HealthDataResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float BodyMass { get; set; }
        public float BMI { get; set; }
        public float BodyFat { get; set; }
        public float LeanBodyMass { get; set; }
        public string? SleepAnalysis { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
