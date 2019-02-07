using System;

namespace Spider.Application.Configurations.Queries.Models
{
    public class ConfigurationLookUp
    {
        public Guid Id { get; set; }
        public string Key { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public Guid EnvironmentId { get; set; }


    }
}