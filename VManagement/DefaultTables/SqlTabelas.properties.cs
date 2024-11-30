using System.Reflection;
using VManagement.Commons.Interfaces;
using VManagement.Commons.Utility;
using VManagement.Core.Entities;
using VManagement.Database;

using static System.ArgumentNullException;

namespace VManagement.Core.DefaultTables
{
    public partial class SqlTabelas : EntityController<SqlTabelas>, IEntity
    {
        public static implicit operator SqlTabelas(long id) => GetFirstOrDefault(id) ?? CreateInstance();

        public string TableName => "SQL_TABELAS";

        public IFieldCollection Fields { get; private set; } = new FieldAggregation();

        public SqlTabelas()
        {
            _dao = new SqlTabelasDAO(this);
        }

        public long Id
        {
            get
            {
                return Fields[FieldNames.ID].ToLong();
            }

            set
            {
                var newInstance = GetFirstOrDefault(value);

                if (newInstance != null)
                    this.Fields = newInstance.Fields;
                else
                    this.Fields[FieldNames.ID] = value;
            }
        }

        public string Name
        {
            get
            {
                return Fields[FieldNames.NAME].SafeToString();
            }

            set
            {
                Fields[FieldNames.NAME] = value;
            }
        }

        public string DotNetObjectName
        {
            get
            {
                return Fields[FieldNames.DOTNETOBJECTNAME].SafeToString();
            }

            set
            {
                Fields[FieldNames.DOTNETOBJECTNAME] = value;
            }
        }

        public string Namespace
        {
            get
            {
                return Fields[FieldNames.NAMESPACE].SafeToString();
            }

            set
            {
                Fields[FieldNames.NAMESPACE] = value;
            }
        }

        public IEnumerable<string> AllFieldNames(bool ignoreId = false)
        {
            var fieldNames = typeof(SqlTabelas.FieldNames)
                 .GetFields(BindingFlags.Public | BindingFlags.Static)
                 .Where(f => f.FieldType == typeof(string))
                 .Select(f => f.GetValue(null).SafeToString());

            if (ignoreId)
                fieldNames = fieldNames.Where(name => name != FieldNames.ID);

            return fieldNames;
        }

        public void ValidateRequiredFields()
        {
            ThrowIfNull(Fields[FieldNames.NAME]);
            ThrowIfNull(Fields[FieldNames.DOTNETOBJECTNAME]);
            ThrowIfNull(Fields[FieldNames.NAMESPACE]);
        }

        public sealed class SqlTabelasDAO(SqlTabelas entity) : EntityDAO<SqlTabelas>(entity)
        {
        }

        public static class FieldNames
        {
            public readonly static string ID = "ID";
            public readonly static string NAME = "NAME";
            public readonly static string DOTNETOBJECTNAME = "DOTNETOBJECTNAME";
            public readonly static string NAMESPACE = "NAMESPACE";
        }
    }
}
