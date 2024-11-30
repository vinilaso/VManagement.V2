namespace VManagement.Commons.Interfaces
{
    public interface IFieldDefinition
    {
        string? Name { get; set; }
        string? PropertyName { get; set; }
        IFieldType Type { get; set; }
        string? FirstLength { get; set; }
        string? LastLength { get; set; }
        bool IsNullable { get; set; }
        bool IsPrimaryKey { get; set; }
        bool IsForeignKey { get; set; }
        string? TableReference { get; set; }
        ICollection<IListItem> ListItems { get; set; }

        string AsCommand();
        string AsPrimaryKey();
        string AsForeignKey();
        string ListName();
    }
}
