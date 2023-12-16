using System;
using System.IO.Enumeration;

namespace webApi.Data.Models
{
    public class SexEnum : SmartEnum<SexEnum, int>
    {
        public static readonly SexEnum Male = new(nameof(Male), 1);
        public static readonly SexEnum Female = new(nameof(Female), 2);
        public static readonly SexEnum Unidentified = new(nameof(Unidentified), 3);

        private SexEnum(string name, int value) : base(name, value)
        {
        }
    }
}
