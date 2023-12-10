using System.ComponentModel.DataAnnotations;

namespace webAPI.DTOs.Response
{
    public class ActivityRequest
    {
        [Required]
        public int Workouts { get; set; }

        [Required]
        public int DailySteps { get; set; }

        [Required]
        public float DailyDistance { get; set; }

        [Required]
        public float DailyEnergyBurned { get; set; }
    }
}
