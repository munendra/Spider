using System;
using MediatR;

namespace Spider.Application.Configurations.Queries.GetConfiguration
{
    public class GetConfigurationQuery : IRequest<ConfigurationViewModel>
    {
        public Guid ConfigurationId { get; }

        public GetConfigurationQuery(Guid configurationId)
        {
            ConfigurationId = configurationId;
        }
    }
}