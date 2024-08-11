namespace Jobeer.Exceptions
{
    public class CantThrowLetterException : Exception
    {
        public CantThrowLetterException()
        {
        }

        public CantThrowLetterException(string message)
            : base(message)
        {
        }

        public CantThrowLetterException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
