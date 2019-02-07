using MediatR;
using Spider.Application.Configurations.Commands.CreateConfiguration;
using Spider.Application.Configurations.Commands.DeleteConfiguration;
using Spider.Application.Configurations.Commands.UpdateConfiguration;
using Spider.Application.Configurations.Queries.GetEnvironmentConfigurations;
using System;
using System.Threading.Tasks;

namespace Spider.Service.Configuration
{
    public class ConfigurationService : BaseService, IConfigurationService
    {
        public ConfigurationService(IMediator mediator) : base(mediator)
        {
        }

        public async Task CreateConfiguration(CreateConfigurationDto createConfiguration)
        {
            await Mediator.Send(new CreateConfigurationCommand(createConfiguration));
        }


        public async Task UpdateConfiguration(UpdateConfigurationDto configuration)
        {
            await Mediator.Send(new UpdateConfigurationCommand(configuration));
        }

        public async Task DeleteConfiguration(Guid configurationId)
        {
            await Mediator.Send(new DeleteConfigurationCommand(configurationId));
        }

        public async Task<ConfigurationViewModel> GetEnvironmentConfigurations(Guid environmentId)
        {
            return await Mediator.Send(new GetEnvironmentConfigurationQuery(environmentId));
        }
    }
}