using System.Reflection;
using VManagement.Commons.Interfaces;
using VManagement.Commons.Utility;
using VManagement.Core.Entities;
using VManagement.Database;

namespace VManagement.Core.DefaultTables
{
    public partial class SqlCampos : EntityController<SqlCampos>, IEntity
    {
        public static implicit operator SqlCampos(long id) => GetFirstOrDefault(id) ?? CreateInstance();

        public string TableName => "SQL_CAMPOS";

        public IFieldCollection Fields { get; set; } = new FieldAggregation();

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

        public string Nome
        {
            get
            {
                return Fields[FieldNames.NOME].SafeToString();
            }

            set
            {
                Fields[FieldNames.NOME] = value;
            }
        }

        public SqlTabelas? Table
        {
            get
            {
                return SqlTabelas.GetFirstOrDefault(Fields[FieldNames.TABLE].ToLong());
            }

            set
            {
                Fields[FieldNames.TABLE] = value?.Id ?? 0;
            }
        }

        public FieldTypeItemList? FieldType
        {
            get
            {
                int index = Fields[FieldNames.FIELDTYPE].ToInt();
                return FieldTypeItemList.Items.FirstOrDefault(item => item.Index == index);
            }
            set
            {
                Fields[FieldNames.FIELDTYPE] = value?.Index ?? 0;
            }
        }

        public bool IsNullable
        {
            get
            {
                return Fields[FieldNames.ISNULLABLE].SafeToString() == "S";
            }

            set
            {
                Fields[FieldNames.ISNULLABLE] = value ? "S" : "N";
            }
        }

        public bool IsPrimaryKey
        {
            get
            {
                return Fields[FieldNames.ISPRIMARYKEY].SafeToString() == "S";
            }

            set
            {
                Fields[FieldNames.ISPRIMARYKEY] = value ? "S" : "N";
            }
        }

        public bool IsForeignKey
        {
            get
            {
                return Fields[FieldNames.ISFOREIGNKEY].SafeToString() == "S";
            }

            set
            {
                Fields[FieldNames.ISFOREIGNKEY] = value ? "S" : "N";
            }
        }

        public string FirstLength
        {
            get
            {
                return Fields[FieldNames.FIRSTLENGTH].SafeToString();
            }

            set
            {
                Fields[FieldNames.FIRSTLENGTH] = value;
            }
        }

        public string LastLength
        {
            get
            {
                return Fields[FieldNames.LASTLENGTH].SafeToString();
            }

            set
            {
                Fields[FieldNames.LASTLENGTH] = value;
            }
        }

        public float Float
        {
            get
            {
                return Fields[FieldNames.FLOAT].ToFloat();
            }
            set
            {
                Fields[FieldNames.FLOAT] = value;
            }
        }

        public SqlTabelas? TableReference
        {
            get
            {
                return SqlTabelas.GetFirstOrDefault(Fields[FieldNames.TABLE].ToLong());
            }

            set
            {
                Fields[FieldNames.TABLE] = value?.Id ?? 0;
            }
        }

        public DateTime DateTime
        {
            get
            {
                return Fields[FieldNames.DATETIME].ToDateTime();
            }
            set
            {
                Fields[FieldNames.DATETIME] = value;
            }
        }

        public IEnumerable<string> AllFieldNames(bool ignoreId = false)
        {
            var fieldNames = typeof(SqlCampos.FieldNames)
                 .GetFields(BindingFlags.Public | BindingFlags.Static)
                 .Where(f => f.FieldType == typeof(string))
                 .Select(f => f.GetValue(null).SafeToString());

            if (ignoreId)
                fieldNames = fieldNames.Where(name => name != FieldNames.ID);

            return fieldNames;
        }

        public void ValidateRequiredFields()
        {

        }

        public sealed class SqlCamposDAO(SqlCampos entity) : EntityDAO<SqlCampos>(entity)
        {
        }

        public static class FieldNames
        {
            public readonly static string ID = "ID";
            public readonly static string NOME = "NOME";
            public readonly static string TABLE = "TABLE";
            public readonly static string FIELDTYPE = "FIELDTYPE";
            public readonly static string ISNULLABLE = "ISNULLABLE";
            public readonly static string ISPRIMARYKEY = "ISPRIMARYKEY";
            public readonly static string ISFOREIGNKEY = "ISFOREIGNKEY";
            public readonly static string TABLEREFERENCE = "TABLEREFERENCE";
            public readonly static string FIRSTLENGTH = "FIRSTLENGTH";
            public readonly static string LASTLENGTH = "LASTLENGTH";
            public readonly static string FLOAT = "FLOAT";
            public readonly static string DATETIME = "DATETIME";
        }

        public sealed class FieldTypeItemList : ItemsList
        {
            public static List<FieldTypeItemList> Items { get; } = new();

            public readonly static FieldTypeItemList ItemBigInt;
            public readonly static FieldTypeItemList ItemBinary;
            public readonly static FieldTypeItemList ItemBit;
            public readonly static FieldTypeItemList ItemChar;
            public readonly static FieldTypeItemList ItemDate;
            public readonly static FieldTypeItemList ItemDateTime;
            public readonly static FieldTypeItemList ItemDateTime2;
            public readonly static FieldTypeItemList ItemDateTimeOffSet;
            public readonly static FieldTypeItemList ItemDecimal;
            public readonly static FieldTypeItemList ItemFloat;
            public readonly static FieldTypeItemList ItemGeography;
            public readonly static FieldTypeItemList ItemGeometry;
            public readonly static FieldTypeItemList ItemHierarchyId;
            public readonly static FieldTypeItemList ItemImage;
            public readonly static FieldTypeItemList ItemInt;
            public readonly static FieldTypeItemList ItemMoney;
            public readonly static FieldTypeItemList ItemNChar;
            public readonly static FieldTypeItemList ItemNText;
            public readonly static FieldTypeItemList ItemNumeric;
            public readonly static FieldTypeItemList ItemNVarChar;
            public readonly static FieldTypeItemList ItemReal;
            public readonly static FieldTypeItemList ItemSmallDateTime;
            public readonly static FieldTypeItemList ItemSmallInt;
            public readonly static FieldTypeItemList ItemSmallMoney;
            public readonly static FieldTypeItemList ItemSqlVariant;
            public readonly static FieldTypeItemList ItemText;
            public readonly static FieldTypeItemList ItemTime;
            public readonly static FieldTypeItemList ItemTimestamp;
            public readonly static FieldTypeItemList ItemTinyInt;
            public readonly static FieldTypeItemList ItemUniqueIdentifier;
            public readonly static FieldTypeItemList ItemVarBinary;
            public readonly static FieldTypeItemList ItemVarChar;
            public readonly static FieldTypeItemList ItemXML;

            static FieldTypeItemList()
            {
                ItemBigInt = new() { Index = 1, Description = "BIGINT" };
                ItemBinary = new() { Index = 2, Description = "BINARY" };
                ItemBit = new() { Index = 3, Description = "BIT" };
                ItemChar = new() { Index = 4, Description = "CHAR" };
                ItemDate = new() { Index = 5, Description = "DATE" };
                ItemDateTime = new() { Index = 6, Description = "DATETIME" };
                ItemDateTime2 = new() { Index = 7, Description = "DATETIME2" };
                ItemDateTimeOffSet = new() { Index = 8, Description = "DATETIMEOFFSET" };
                ItemDecimal = new() { Index = 9, Description = "DECIMAL" };
                ItemFloat = new() { Index = 10, Description = "FLOAT" };
                ItemGeography = new() { Index = 11, Description = "GEOGRAPHY" };
                ItemGeometry = new() { Index = 12, Description = "GEOMETRY" };
                ItemHierarchyId = new() { Index = 13, Description = "HIERARCHYID" };
                ItemImage = new() { Index = 14, Description = "IMAGE" };
                ItemInt = new() { Index = 15, Description = "INT" };
                ItemMoney = new() { Index = 16, Description = "MONEY" };
                ItemNChar = new() { Index = 17, Description = "NCHAR" };
                ItemNText = new() { Index = 18, Description = "NTEXT" };
                ItemNumeric = new() { Index = 19, Description = "NUMERIC" };
                ItemNVarChar = new() { Index = 20, Description = "NVARCHAR" };
                ItemReal = new() { Index = 21, Description = "REAL" };
                ItemSmallDateTime = new() { Index = 22, Description = "SMALLDATETIME" };
                ItemSmallInt = new() { Index = 23, Description = "SMALLINT" };
                ItemSmallMoney = new() { Index = 24, Description = "SMALLMONEY" };
                ItemSqlVariant = new() { Index = 25, Description = "SQLVARIANT" };
                ItemText = new() { Index = 26, Description = "TEXT" };
                ItemTime = new() { Index = 27, Description = "TIME" };
                ItemTimestamp = new() { Index = 28, Description = "TIMESTAMP" };
                ItemTinyInt = new() { Index = 29, Description = "TINYINT" };
                ItemUniqueIdentifier = new() { Index = 30, Description = "UNIQUEIDENTIFIER" };
                ItemVarBinary = new() { Index = 31, Description = "VARBINARY" };
                ItemVarChar = new() { Index = 32, Description = "VARCHAR" };
                ItemXML = new() { Index = 33, Description = "XML" };


                var fieldTypes = typeof(FieldTypeItemList)
                                 .GetFields(BindingFlags.Static | BindingFlags.Public)
                                 .Where(field => field.FieldType == typeof(FieldTypeItemList))
                                 .Select(field => (FieldTypeItemList)field.GetValue(null));

                foreach (var field in fieldTypes)
                {
                    Items.Add(field);
                }
            }
        }
    }
}