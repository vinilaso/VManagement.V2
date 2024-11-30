using System.Data.SqlClient;
using VManagement.Commons.Interfaces;
using VManagement.Commons.Utility;

namespace VManagement.Database.SqlClauses
{
    public class CommandBuilder
    {
        private const string ALIAS = "A";
        private IEntity _entity { get; }
        public Restriction Restriction { get; set; } = Restriction.Empty;
        public SqlCommand? Command { get; set; }

        public string SelectClause => BuildSelectClause();
        public string UpdateClause => BuildUpdateClause();
        public string DeleteClause => BuildDeleteClause();
        public string InsertClause => BuildInsertClause();
        public string CreateTableClause => BuildCreateTableClause();

        public CommandBuilder(IEntity entity)
        {
            _entity = entity;
        }

        public CommandBuilder(IEntity entity, SqlCommand command)
        {
            _entity = entity;
            Command = command;
        }

        private string BuildSelectClause()
        {
            var builder = new DelimitedStringBuilder("SELECT");

            builder.Append(string.Join(", ", _entity.AllFieldNames()))
                   .Append("FROM")
                   .Append(_entity.TableName)
                   .Append(ALIAS);

            if (Restriction != Restriction.Empty)
            {
                builder.Append(Restriction.ToString());
            }

            ArgumentNullException.ThrowIfNull(Command, nameof(Command));
            Restriction.SetParameters(Command);

            ClearRestriction();
            return builder.ToString();
        }

        private string BuildUpdateClause()
        {
            ClearRestriction();
            return string.Empty;
        }

        private string BuildDeleteClause()
        {
            ClearRestriction();
            return string.Empty;
        }

        private string BuildInsertClause()
        {
            var builder = new DelimitedStringBuilder();
            var fieldNames = _entity.AllFieldNames(ignoreId: true);

            builder.Append("INSERT INTO")
                   .Append(_entity.TableName)
                   .OpenParenthesis()
                   .AppendJoin(", ", fieldNames)
                   .CloseParenthesis()
                   .Append("OUTPUT INSERTED.ID")
                   .Append("VALUES")
                   .OpenParenthesis()
                   .AppendJoin(", ",fieldNames.Select(name => name.AsParameter()))
                   .CloseParenthesis();

            Restriction restriction = Restriction.Empty;
            foreach (var field in fieldNames)
            {
                restriction.Parameters.Add(field.AsParameter(), _entity.Fields[field]);
            }

            restriction.SetParameters(Command);

            ClearRestriction();
            return builder.ToString();
        }

        private string BuildCreateTableClause()
        {
            var builder = new DelimitedStringBuilder();

            builder.Append("CREATE TABLE ");

            return builder.ToString();
        }

        private void ClearRestriction()
        {
            Restriction = Restriction.Empty;
        }
    }
}
