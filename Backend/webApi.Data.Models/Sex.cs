using System;
using System.IO.Enumeration;

namespace webApi.Data.Models
{
    public class Sex : SmartEnum<Sex>
    {
        public static readonly Sex Male = new(nameof(Male), 1);
        public static readonly Sex Female = new(nameof(Female), 2);
        public static readonly Sex Unidentified = new(nameof(Unidentified), 3);

        public Sex(string name, int value) : base(name, value)
        {
        }
    }
}
