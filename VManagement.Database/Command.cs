namespace VManagement.Database
{
    public class Command
    {
        public static void Execute(string command)
        {
            using var connection = new DatabaseConnection();
            var cmd = connection.CreateCommand();

            cmd.CommandText = command;
            cmd.ExecuteNonQuery();
        }

        public string Statement { get; set; } = string.Empty;
        public Command(string statement)
        {
            Statement = statement;
        }

        public void Execute()
        {
            Execute(Statement);
        }
    }
}
