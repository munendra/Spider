using System;

namespace Spider.Application.Configurations.Commands.Models
{
    public class ConfigurationDto
    {
        public Guid EnvironmentId { get; set; }
        public string Key { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}