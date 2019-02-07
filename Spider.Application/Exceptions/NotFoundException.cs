using Remotion.Linq.Parsing.Structure.ExpressionTreeProcessors;

namespace Spider.Application.Exceptions
{
    public class NotFoundException : SpiderException
    {
        public NotFoundException() : base()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}