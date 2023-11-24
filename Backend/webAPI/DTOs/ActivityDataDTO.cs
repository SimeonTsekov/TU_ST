using webAPI.DTOs.Request;

namespace webAPI.DTOs
{
    public class ActivityDataDTO
    {
        public int ActivityDataId { get; set; }
        public int UserId { get; set; }
        public int Workouts { get; set; }
        public int DailySteps { get; set; }
        public float DailyDistance { get; set; }
        public float DailyEnergyBurned { get; set; }
        // Navigation property to the User
        public virtual UserDTO? User { get; set; }
    }
}
