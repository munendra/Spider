using System;

namespace Spider.Application.Exceptions
{
    public class RecordAlreadyExistsException : SpiderException
    {
        public RecordAlreadyExistsException(string message) : base(message)
        {
        }

        public RecordAlreadyExistsException() : base()
        {
        }
    }
}