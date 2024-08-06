namespace Jobeer.Exceptions
{
    public class BotExcention : Exception
    {
        public BotExcention()
        {
        }

        public BotExcention(string message)
            : base(message)
        {
        }

        public BotExcention(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
