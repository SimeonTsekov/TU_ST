namespace webApi.Data.Models
{
    public class RolesEnum : SmartEnum<RolesEnum, int>
    {
        public static readonly RolesEnum User = new(nameof(User), 1);
        public static readonly RolesEnum Admin = new(nameof(Admin), 2);

        private RolesEnum(string name, int value) : base(name, value)
        {
        }
    }
}
