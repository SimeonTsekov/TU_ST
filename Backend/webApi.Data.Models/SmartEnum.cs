using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace webApi.Data.Models
{
    public class SmartEnum<TEnum> : BaseModel where TEnum : SmartEnum<TEnum>
    {
        private static readonly Dictionary<int, TEnum> Enumerations = CreateEnumerations();

        public string Name { get; set; }

        public int Value { get; set; }

        protected SmartEnum(string name, int value)
        {
            Name = name;
            Value = value;
            Id = Value;
        }

        public static TEnum? FromValue(int value)
        {
            return (Enumerations
                .TryGetValue(value, out var result)
                ? result
                : null);
        }

        public static TEnum? FromName(string name)
        {
            return Enumerations
                .Values
                .SingleOrDefault(state => state.Name == name);
        }

        public static IEnumerable<TEnum> GetValues()
        {
            var fieldsForType = typeof(TEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(field => field.FieldType == typeof(TEnum))
                .Select(field => (TEnum)field.GetValue(null)!);

            return fieldsForType;
        }

        private static Dictionary<int, TEnum> CreateEnumerations()
        {
            return GetValues().ToDictionary(x => x.Value);
        }
    }
}
