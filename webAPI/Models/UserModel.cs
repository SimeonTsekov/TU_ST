namespace webAPI.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Passwords { get; set; } // Note: Storing passwords as plain text is not secure
        public int Age { get; set; }
        public int Height { get; set; }
        // Navigation property for related ActivityData records
        public virtual ICollection<ActivityDataModel> ActivityData { get; set; }
        // Navigation property for related HealthData records
        public virtual ICollection<HealthDataModel> HealthData { get; set; }
    }
}
