using System.ComponentModel.DataAnnotations;

namespace webAPI.DTOs.Response
{
    public class ActivityRequest
    {
        [Range(0, int.MaxValue, ErrorMessage = "Workouts must be a positive number.")]
        public int Workouts { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "DailySteps must be a positive number.")]
        public int DailySteps { get; set; }

        [Range(0f, float.MaxValue, ErrorMessage = "DailyDistance must be a positive number.")]
        public float DailyDistance { get; set; }

        [Range(0f, float.MaxValue, ErrorMessage = "DailyEnergyBurned must be a positive number.")]
        public float DailyEnergyBurned { get; set; }
    }
}
