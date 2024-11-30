using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VManagement.Commons.Interfaces
{
    public interface IFieldCollection : ICollection<IField>
    {
        object? this[string name] { get; set; }

        void Add(string name, object? value);
    }
}
