namespace Application.Common.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException()
            : base()
        {
        }

        public DatabaseException(string message)
            : base(message)
        {
        }
    }
}