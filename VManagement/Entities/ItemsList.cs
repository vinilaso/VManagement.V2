namespace VManagement.Core.Entities
{
    public class ItemsList
    {
        public static implicit operator int(ItemsList item) => item.Index;
        public static implicit operator ItemsList(int index) => new(index, string.Empty);

        public int Index { get; set; }
        public string Description { get; set; } = string.Empty;

        public ItemsList() { }

        public ItemsList(int index, string description)
        {
            Index = index;
            Description = description;
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
