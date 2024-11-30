using VManagement.Commons.Utility;

namespace VManagement.Core.Entities
{
    public class Entity
    {
        public readonly string Name = string.Empty;
        public FieldAggregation Fields { get; private set; } = new();
        
        public long Id
        {
            get { return Fields["ID"].ToLong(); }
            set
            {
                Fields["ID"] = value;
            }
        }
    }
}
