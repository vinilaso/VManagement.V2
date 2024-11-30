using System.Data.SqlClient;

namespace VManagement.Database
{
    public sealed class DatabaseConnection : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction? _transaction;

        public bool InTransaction => _transaction != null;

        public DatabaseConnection()
        {
            _connection = new SqlConnection(Security.Instance.ConnectionString);
            _connection.Open();
        }
        
        public SqlCommand CreateCommand() => _connection.CreateCommand();

        public void BeginTransaction()
        {
            if (_transaction == null)
                _transaction = _connection.BeginTransaction();
            else
                throw new InvalidOperationException("A transação já foi iniciada!");
        }

        public void CommitTransaction() 
        {
            if ( _transaction != null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            RollbackTransaction();

            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }

            _connection.Dispose();
        }
    }
}
