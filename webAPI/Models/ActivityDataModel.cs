namespace webAPI.Models
{
    public class ActivityDataModel
    {
        public int ActivityDataId { get; set; }
        public int UserId { get; set; }
        public int Workouts { get; set; }
        public int DailySteps { get; set; }
        public float DailyDistance { get; set; }
        public float DailyEnergyBurned { get; set; }
        // Navigation property to the User
        public virtual UserModel User { get; set; }
    }
}
