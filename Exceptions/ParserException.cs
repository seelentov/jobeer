namespace Jobeer.Exceptions
{
    public class NotFoundFileException : Exception
    {
        public NotFoundFileException()
        {
        }

        public NotFoundFileException(string message)
            : base(message)
        {
        }

        public NotFoundFileException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
