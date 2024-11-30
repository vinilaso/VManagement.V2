namespace VManagement.Commons.Interfaces
{
    public interface IFieldType
    {
        string? Type { get; set; }
        string? Name { get; set; } 
        bool NeedsLength { get; set; }
        bool IsDoubleLength { get; set; }
        bool IsList { get; set; }

        string SqlType();
    }
}
