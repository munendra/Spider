using System;
using System.Net;

namespace Spider.Application.Exceptions
{
    public class SpiderException : Exception
    {
        public int StatusCode { get; }
        public SpiderException(string message,int statusCode= (int)HttpStatusCode.BadRequest) : base(message)
        {
            StatusCode = statusCode;
        }


        public SpiderException() : base()
        {
        }
    }
}