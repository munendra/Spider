using System;
using MediatR;

namespace Spider.Application.Configurations.Queries.GetEnvironmentConfigurations
{
    public class GetEnvironmentConfigurationQuery : IRequest<ConfigurationViewModel>
    {
        public Guid EnvironmentId { get; }

        public GetEnvironmentConfigurationQuery(Guid environmentId)
        {
            EnvironmentId = environmentId;
        }
    }
}