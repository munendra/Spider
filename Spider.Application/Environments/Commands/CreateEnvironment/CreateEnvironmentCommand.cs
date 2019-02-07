using MediatR;
using Spider.Application.Environments.Commands.Model;

namespace Spider.Application.Environments.Commands.CreateEnvironment
{
    public class CreateEnvironmentCommand : IRequest
    {
        public CreateEnvironmentDto Request { get; }

        public CreateEnvironmentCommand(CreateEnvironmentDto request)
        {
            Request = request;
        }
    }
}