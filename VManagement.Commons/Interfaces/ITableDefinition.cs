namespace VManagement.Commons.Interfaces
{
    public interface ITableDefinition
    {
        string? Name { get; set; }
        string? DotNetObjectName { get; set; }
        string? Namespace { get; set; }
        ICollection<IFieldDefinition> Fields { get; set; }

        string AsCreateCommand();
    }
}
