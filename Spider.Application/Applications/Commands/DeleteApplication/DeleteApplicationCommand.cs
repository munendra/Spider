using System;
using MediatR;

namespace Spider.Application.Applications.Commands.DeleteApplication
{
    public class DeleteApplicationCommand : IRequest
    {
        public Guid ApplicationId { get; set; }

        public DeleteApplicationCommand(Guid applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}