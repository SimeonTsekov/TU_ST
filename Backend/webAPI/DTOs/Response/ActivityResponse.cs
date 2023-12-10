namespace webAPI.DTOs.Response
{
    public class ActivityResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Workouts { get; set; }
        public int DailySteps { get; set; }
        public float DailyDistance { get; set; }
        public float DailyEnergyBurned { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
