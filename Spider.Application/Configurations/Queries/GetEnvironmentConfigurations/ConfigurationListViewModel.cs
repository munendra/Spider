using System.Collections.Generic;
using Spider.Application.Configurations.Queries.Models;

namespace Spider.Application.Configurations.Queries.GetEnvironmentConfigurations
{
    public class ConfigurationViewModel
    {
        public ConfigurationViewModel()
        {
            Configurations = new List<ConfigurationLookUp>();
        }

        public IEnumerable<ConfigurationLookUp> Configurations { get; set; }
    }
}