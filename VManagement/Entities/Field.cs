using VManagement.Commons.Interfaces;

namespace VManagement.Core.Entities
{
    public class Field : IField
    {
        public string Name { get; set; } = string.Empty;
        public object? Value { get; set; } = null;

        public Field() { }
        public Field(string name, object? value)
        {
            Name = name;
            Value = value;
        }

        public string AsParameter()
        {
            return string.Format("@{0}", Name);
        }

        public override string ToString() => 
            Value?.ToString() ?? string.Empty;
    }
}
