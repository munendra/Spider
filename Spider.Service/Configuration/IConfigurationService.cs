using Spider.Application.Configurations.Commands.CreateConfiguration;
using Spider.Application.Configurations.Commands.UpdateConfiguration;
using Spider.Application.Configurations.Queries.GetEnvironmentConfigurations;
using System;
using System.Threading.Tasks;

namespace Spider.Service.Configuration
{
    public interface IConfigurationService
    {
        Task<ConfigurationViewModel> GetEnvironmentConfigurations(Guid environmentId);
        Task CreateConfiguration(CreateConfigurationDto createConfiguration);
        Task UpdateConfiguration(UpdateConfigurationDto configuration);
        Task DeleteConfiguration(Guid configurationId);
    }
}