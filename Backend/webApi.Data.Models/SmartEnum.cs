namespace webApi.Data.Models
{
    public abstract class SmartEnum<TEnum, TValue> where TEnum : SmartEnum<TEnum, TValue>
    {
        public string Name { get; }
        public TValue Value { get; }

        protected SmartEnum(string name, TValue value)
        {
            Name = name;
            Value = value;
        }
    }
}
