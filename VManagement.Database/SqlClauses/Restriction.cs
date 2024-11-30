using System.Data.SqlClient;
using System.Text;
using VManagement.Commons.Utility;

namespace VManagement.Database.SqlClauses
{
    public class Restriction
    {
        public static readonly Restriction Empty;

        public ParameterCollection Parameters { get; private set; } = new();
        public string SortClause  { get; set; } = string.Empty;
        public string WhereClause { get; set; } = string.Empty;
        public string GroupClause { get; set; } = string.Empty;

        static Restriction()
        {
            Empty = new Restriction();
        }

        public Restriction() { }

        public Restriction(string whereClause)
        {
            this.AddWhere(whereClause);
        }

        public void AddWhere(string whereClause)
        {
            var sb = new DelimitedStringBuilder();

            if (WhereClause.IsNullOrEmpty())
                sb.Append("WHERE");
            else
                sb.Append("AND");

            sb.Append(whereClause);
            WhereClause = sb.ToString();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (!WhereClause.IsNullOrEmpty())
                builder.Append(WhereClause);

            if (!GroupClause.IsNullOrEmpty())
                builder.Append(GroupClause);

            if (!SortClause.IsNullOrEmpty())
                builder.Append(SortClause);

            return builder.ToString();
        }

        public void SetParameters(SqlCommand targetCommand)
        {
            foreach (var parameter in Parameters)
                targetCommand.Parameters.Add(parameter.AsSqlParameter());
        }

        public static Restriction FromId(long id)
        {
            var restricao = new Restriction("A.ID = @ID");
            restricao.Parameters.Add("@ID", id);

            return restricao;
        }
    }
}
