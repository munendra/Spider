using MediatR;
using System;

namespace Spider.Application.Environments.Queries.GetApplicationEnvironments
{
    public class GetApplicationEnvironmentsQuery : IRequest<EnvironmentListViewModel>
    {
        public Guid ApplicationId { get; }

        public GetApplicationEnvironmentsQuery(Guid applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}