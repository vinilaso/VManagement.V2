using System.Data;
using VManagement.Commons.Interfaces;
using VManagement.Database;
using VManagement.Database.SqlClauses;

namespace VManagement.Core.Entities
{
    public abstract class EntityController<T> where T : IEntity, new()
    {
        /// <summary>
        /// O nome da entidade no banco de dados.
        /// </summary>
        public static string EntityName => new T().TableName;
        protected EntityDAO<T>? _dao;
        
        /// <summary>
        /// Cria uma instância da entidade.
        /// </summary>
        /// <returns></returns>
        public static T CreateInstance()
        {
            T instance = new();

            foreach(var field in instance.AllFieldNames())
                instance.Fields.Add(field, null);

            return instance;
        }

        public static T? GetFirstOrDefault(long id)
        {
            return EntityDAO<T>.GetFirstOrDefault(Restriction.FromId(id));
        }

        public static T? GetFirstOrDefault(Restriction restriction)
        {
            return EntityDAO<T>.GetFirstOrDefault(restriction);
        }

        public static ICollection<T> GetMany(Restriction restriction)
        {
            return EntityDAO<T>.GetMany(restriction);
        }

        public static bool Exists(long id)
        {
            return EntityDAO<T>.Exists(Restriction.FromId(id));
        }

        public static bool Exists(Restriction restriction)
        {
           return EntityDAO<T>.Exists(restriction);
        }

        public virtual void Save()
        {
            _dao?.Entity.ValidateRequiredFields();
            Saving();
        }

        protected virtual void Saving()
        {
            if (_dao == null)
                throw new NullReferenceException(nameof(_dao));

            _dao.Save();
            Saved();
        }

        protected virtual void Saved()
        {

        }
    }
}
