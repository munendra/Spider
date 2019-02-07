using MediatR;

namespace Spider.Service
{
    public class BaseService
    {
        public IMediator Mediator { get; }

        public BaseService(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}