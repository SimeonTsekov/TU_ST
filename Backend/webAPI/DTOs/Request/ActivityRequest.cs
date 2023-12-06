using webAPI.DTOs.Request;

namespace webAPI.DTOs.Response
{
    public class ActivityRequest
    {
        public int Workouts { get; set; }
        public int DailySteps { get; set; }
        public float DailyDistance { get; set; }
        public float DailyEnergyBurned { get; set; }
    }
}
