using System.Collections;
using VManagement.Commons.Interfaces;

namespace VManagement.Core.Entities
{
    public class FieldAggregation : IFieldCollection
    {
        #region [ Properties ]
        private ICollection<IField> Fields { get; set; } = new List<IField>();

        public int Count => Fields.Count;

        public bool IsReadOnly => Fields.IsReadOnly;

        public object? this[string name]
        {
            get
            {
                return Fields.FirstOrDefault(f => f.Name == name)?.Value;
            }
            set
            {
                var field = Fields.FirstOrDefault(f => f.Name == name);

                if (field == null)
                    throw new ArgumentException($"Não há campos com o nome {name}");

                field.Value = value;
            }
        }
        #endregion

        public void Add(IField item)
        {
            VerifyDuplicity(item.Name);
            Fields.Add(item);
        }

        public void Add(string name, object? value) =>
            this.Add(new Field(name, value));

        public void Clear() =>
            Fields.Clear();

        public bool Contains(IField item) =>
            Fields.Contains(item);

        public bool Contains(string name) =>
            Fields.Any(f => f.Name == name);

        public void CopyTo(IField[] array, int arrayIndex) =>
            Fields.CopyTo(array, arrayIndex);

        public IEnumerator<IField> GetEnumerator() =>
            Fields.GetEnumerator();

        public bool Remove(IField item) =>
            Fields.Remove(item);

        IEnumerator IEnumerable.GetEnumerator() =>
            Fields.GetEnumerator();

        private void VerifyDuplicity(string name)
        {
            if (Fields.Any(f => f.Name == name))
                throw new ArgumentException($"Já existe um campo com o nome {name}!");
        }
    }
}
