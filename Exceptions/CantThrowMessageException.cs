namespace Jobeer.Exceptions
{
    public class CantThrowMessageException : Exception
    {
        public CantThrowMessageException()
        {
        }

        public CantThrowMessageException(string message)
            : base(message)
        {
        }

        public CantThrowMessageException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
