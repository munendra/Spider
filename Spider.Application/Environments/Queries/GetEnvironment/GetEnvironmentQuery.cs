using System;
using MediatR;
using Spider.Application.Environments.Queries.Models;

namespace Spider.Application.Environments.Queries.GetEnvironment
{
    public class GetEnvironmentQuery:IRequest<EnvironmentLookUpModel>
    {
        public Guid EnvironmentId { get; }

        public GetEnvironmentQuery(Guid environmentId)
        {
            EnvironmentId = environmentId;
        }
    }
}