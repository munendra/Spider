namespace Spider.Application.Exceptions
{
    public class InvalidCommandException : SpiderException
    {
        //ToDo:: Add Log
        public InvalidCommandException(string message) : base(message)
        {
        }
    }
}