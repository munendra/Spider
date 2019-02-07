using System;
using MediatR;

namespace Spider.Application.Configurations.Commands.DeleteConfiguration
{
    public class DeleteConfigurationCommand : IRequest
    {
        public Guid ConfigurationId { get; }

        public DeleteConfigurationCommand(Guid configurationId)
        {
            ConfigurationId = configurationId;
        }
    }
}