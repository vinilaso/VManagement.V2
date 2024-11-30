using System.Data.SqlClient;
using VManagement.Commons.Utility;

namespace VManagement.Database
{
    public sealed class Security
    {
        public static Security Instance { get; }

        static Security()
        {
            Instance = new Security();
        }

        private Security() { }

        public string ConnectionString { get; private set; } = string.Empty;

        public void SetConnectionString(string filePath)
        {
            var builder = new SqlConnectionStringBuilder();

            using var file = File.OpenRead(filePath);
            using var reader = new StreamReader(file);

            string fileContent = reader.ReadToEnd();

            builder.DataSource     = fileContent.FindValue("DataSource");
            builder.InitialCatalog = fileContent.FindValue("InitialCatalog");
            builder.Password       = fileContent.FindValue("Password");
            builder.UserID         = fileContent.FindValue("UserID");
            builder.TrustServerCertificate = true;

            ConnectionString = builder.ToString();
            
        }

        public static void SetupEnvironment()
        {
            Instance.SetConnectionString("C:\\Users\\Vini\\source\\repos\\VManagement\\Arquivos\\connection_string.txt");
        }
        
        public static bool TestConnection()
        {
            return true;
        }
    }
}
