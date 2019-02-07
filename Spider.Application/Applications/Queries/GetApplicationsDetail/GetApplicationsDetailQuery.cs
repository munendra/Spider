using System;
using MediatR;

namespace Spider.Application.Applications.Queries.GetApplicationsDetail
{
    public class GetApplicationsDetailQuery : IRequest<ApplicationsDetailViewModel>
    {
        public Guid ApplicationId { get; }

        public GetApplicationsDetailQuery(Guid applicationId)
        {
            ApplicationId = applicationId;
        }
    }
}