using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Spider.Application.Configurations.Commands.CreateConfiguration;
using Spider.Application.Configurations.Commands.UpdateConfiguration;
using Spider.Service.Configuration;

namespace Spider.Api.Controllers
{
    [Route("api")]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpGet]
        [Route("environment/{environmentId}/configurations")]
        public async Task<IActionResult> GetEnvironmentConfiguration(Guid environmentId)
        {
            var configurations = await _configurationService.GetEnvironmentConfigurations(environmentId);
            return Ok(configurations);
        }

        [HttpPost]
        public async Task<IActionResult> AddConfiguration(CreateConfigurationDto createConfiguration)
        {
            await _configurationService.CreateConfiguration(createConfiguration);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateConfiguration(UpdateConfigurationDto updateConfiguration)
        {
            await _configurationService.UpdateConfiguration(updateConfiguration);
            return Ok();
        }

        [HttpDelete("{configurationId}")]
        public async Task<IActionResult> Delete(Guid configurationId)
        {
            await _configurationService.DeleteConfiguration(configurationId);
            return Ok();
        }
    }
}