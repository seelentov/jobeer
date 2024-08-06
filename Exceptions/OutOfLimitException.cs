namespace Jobeer.Exceptions
{
    public class OutOfLimitException : Exception
    {
        public OutOfLimitException()
        {
        }

        public OutOfLimitException(string message)
            : base(message)
        {
        }

        public OutOfLimitException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
