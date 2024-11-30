using System.Data;
using VManagement.Commons.Attributes;
using VManagement.Commons.Interfaces;
using VManagement.Database.SqlClauses;

namespace VManagement.Database
{
    public abstract class EntityDAO<T> where T : IEntity, new()
    {
        private CommandBuilder? _commandBuilder { get; }
        public IEntity Entity { get; set; }

        public EntityDAO() { }

        public EntityDAO(IEntity entity)
        {
            Entity = entity;
        }

        public void Save()
        {
            using var connection = new DatabaseConnection();
            try
            {
                var cmd = connection.CreateCommand();

                var cmdBuilder = new CommandBuilder(Entity, cmd);
                cmd.CommandText = cmdBuilder.InsertClause;

                using var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Entity.Id = reader.GetInt64(0);
                }
            }
            catch
            {
                connection.RollbackTransaction();
            }
        }

        [UseWithCaution("Este método ignora regras de negócio, e deve ser utilizado apenas quando estritamente necessário.")]
        public static bool Save(IEntity entity)
        {
            return true;
        }

        private static T CreateTypeInstance()
        {
            T instance = new();

            foreach (var field in instance.AllFieldNames())
                instance.Fields.Add(field, null);

            return instance;
        }

        public static T? GetFirstOrDefault(Restriction restriction)
        {
            var entity = CreateTypeInstance();
            using var connection = new DatabaseConnection();
            var cmd = connection.CreateCommand();

            var cmdBuilder = new CommandBuilder(entity, cmd)
            {
                Restriction = restriction
            };

            cmd.CommandText = cmdBuilder.SelectClause;

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                foreach (string field in entity.AllFieldNames())
                {
                    entity.Fields[field] = reader.GetValue(field);
                }

                return entity;
            }

            return default;
        }

        public static ICollection<T> GetMany(Restriction restriction)
        {
            var entity = CreateTypeInstance();
            using var connection = new DatabaseConnection();
            var cmd = connection.CreateCommand();

            var cmdBuilder = new CommandBuilder(entity, cmd)
            {
                Restriction = restriction
            };

            cmd.CommandText = cmdBuilder.SelectClause;

            using var reader = cmd.ExecuteReader();

            ICollection<T> result = new List<T>();

            while (reader.Read())
            {
                entity = CreateTypeInstance();

                foreach (string field in entity.AllFieldNames())
                {
                    entity.Fields[field] = reader.GetValue(field);
                }

                result.Add(entity);
            }

            return result;
        }

        public static bool Exists(Restriction restriction)
        {
            var entity = CreateTypeInstance();

            using var connection = new DatabaseConnection();
            var cmd = connection.CreateCommand();

            var cmdbuilder = new CommandBuilder(entity, cmd)
            {
                Restriction = restriction
            };

            cmd.CommandText = cmdbuilder.SelectClause;

            using var reader = cmd.ExecuteReader();
            return reader.Read();
        }
    }
}
