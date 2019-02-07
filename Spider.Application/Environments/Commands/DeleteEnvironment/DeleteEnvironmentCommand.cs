using System;
using MediatR;

namespace Spider.Application.Environments.Commands.DeleteEnvironment
{
    public class DeleteEnvironmentCommand : IRequest
    {
        public Guid EnvironmentId { get; }

        public DeleteEnvironmentCommand(Guid environmentId)
        {
            EnvironmentId = environmentId;
        }
    }
}