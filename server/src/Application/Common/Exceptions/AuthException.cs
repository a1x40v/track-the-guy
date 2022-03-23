namespace Application.Common.Exceptions
{
    public class AuthException : Exception
    {
        public AuthException()
            : base()
        {
        }

        public AuthException(string message)
            : base(message)
        {
        }
    }
}