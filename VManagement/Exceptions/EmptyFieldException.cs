namespace VManagement.Core.Exceptions
{
    public class EmptyFieldException : Exception
    {
        public EmptyFieldException() : base() { }
        public EmptyFieldException(string message) : base(message) { }

        public static void ThrowIfNull(object? field, string name)
        {
            if (field == null)
            {
                throw new EmptyFieldException($"O campo {name} é obrigatório!");
            }
        }
    }
}
