using System;
using Spider.Application.Configurations.Commands.Models;

namespace Spider.Application.Configurations.Commands.UpdateConfiguration
{
    public class UpdateConfigurationDto : ConfigurationDto
    {
        public Guid Id { get; set; }
    }
}