namespace Spider.Application.Exceptions
{
    public class InvalidInputException : SpiderException
    {
        public InvalidInputException(string message) : base(message)
        {
        }
    }
}